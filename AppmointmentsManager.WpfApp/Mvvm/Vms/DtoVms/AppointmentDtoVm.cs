using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.Messages;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

[ObservableRecipient]
public partial class AppointmentDtoVm : DtoVmBase
{
    private bool _initialized = false;

    private Appointment _appointment = new();
    private readonly ILoginService _login;

    public AppointmentDtoVm(IDataService data,
        IDialogService dialog,
        IMessenger messenger,
        ILoginService login) : base(data, dialog)
    {
        Messenger = messenger;
        _login = login;
        Overlaps.CollectionChanged += (_, _) => ValidateTimes();
    }

    partial void OnStartChanging(DateTime value) => CheckOverlaps(value, End);

    partial void OnEndChanging(DateTime value) => CheckOverlaps(Start, value);

    partial void OnStartChanged(DateTime oldValue, DateTime newValue)
    {
        CheckAppointmentDates(oldValue, newValue);
        ValidateProperty(End, nameof(End));
    }

    partial void OnEndChanged(DateTime oldValue, DateTime newValue)
    {
        CheckAppointmentDates(oldValue, newValue);
        ValidateProperty(Start, nameof(Start));
    }

    public override DbModel DbModel => _appointment;

    [ObservableProperty]
    private bool _isModified;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [Required]
    [NotifyDataErrorInfo]
    private Customer? _customer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [Required]
    [NotifyDataErrorInfo]
    private User? _user;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [CustomValidation(typeof(AppointmentDtoVm), nameof(ValidateTime))]
    [Required]
    [NotifyDataErrorInfo]
    private DateTime _start;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(AppointmentDtoVm), nameof(ValidateTime))]
    [Required]
    private DateTime _end;

    [ObservableProperty]
    [Required(ErrorMessage = "Appointment Type is required.")]
    [NotifyDataErrorInfo]
    private string? _type;
 
    public IEnumerable<DateOnly> AppointmentDates
    {
        get
        {
            var startDate = DateOnly.FromDateTime(Start);
            var endDate = DateOnly.FromDateTime(End);
            return startDate == endDate ? Enumerable.Repeat(startDate, 1) : new[] { startDate, endDate };
        }
    }

    public bool IsOnDate(DateOnly date) => DateOnly.FromDateTime(Start) == date || DateOnly.FromDateTime(End) == date;


    public ObservableCollection<AppointmentDtoVm> Appointments { get; set; } = new();

    public ReadOnlyObservableCollection<Customer> Customers => _data.Customers;

    public ReadOnlyObservableCollection<User> Users => _data.Users;

    public string DisplayName => $"{GetFormattedDateTime(Start)} - {GetFormattedDateTime(End)}, " +
        $"User: {User?.userName ?? "N/A"}, " +
        $"Customer: {Customer?.customerName ?? "N/A"}";

    private ObservableCollection<AppointmentDtoVm> Overlaps { get; } = new();

    public void ValidateProperties() => ValidateAllProperties();

    public void CheckOverlaps() => CheckOverlaps(Start, End);

    public static ValidationResult? ValidateTime(object? value, ValidationContext context)
    {
        var instance = (AppointmentDtoVm)context.ObjectInstance;
        var time = (DateTime)value!;
        if (instance.Overlaps.Count > 0)
            return new($"Times are overlapping with another appointment for this user");
        if (!CheckOpenHours(time))
            return new($"Time is outside of bounds 9AM - 5PM Eastern Standard Time.");
        if (!CheckOpenDays(time))
            return new($"Date is outside of bounds Monday-Friday.");
        if (instance.Start > instance.End) return new("Start time is after end time.");

        return ValidationResult.Success;

    }


    [RelayCommand(CanExecute = (nameof(CanSave)))]
    public void Save()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            _dialog.ShowMessage("This appointment has input errors. Please resolve before saving", "Invalid Appointment");
            return;
        }
        SaveEntity();
        if (SaveToDb())
        {
            OnPropertyChanged(nameof(IsInDb));
            IsModified = false;
            Messenger.Send(new AppointmentSavedMessage(this));
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        if (IsInDb)
        {
            LoadEntity(_appointment);
            IsModified = !IsInDb;
            return;
        }
        else
        {
            IsModified = false;
            foreach (var apt in Overlaps.ToArray())
            {
                apt.RemoveOverlap(this);
                apt.ValidateTimes();
            }
            Messenger.Send(new NewAppointmentDiscardedMessage(this));
        }
    }

    public override void InitEmpty()
    {
        var now = DateTime.Now;
        _appointment = new()
        {
            title = string.Empty,
            description = string.Empty,
            location = string.Empty,
            contact = string.Empty,
            url = string.Empty,
            User = _login.LoggedInUser,
            start = now,
            end = now.AddHours(1)
        };
        LoadEntity(_appointment);
        IsModified = true;
    }

    public override void LoadEntity(DbModel entity)
    {
        if (entity is not Appointment appointment) return;
        base.LoadEntity(entity);
        _appointment = appointment;
        User = _appointment.User;
        Type = _appointment.type;
        Start = _appointment.start.ToLocalTime();
        End = _appointment.end.ToLocalTime();
        Customer = _appointment.Customer;
        _initialized = true;
        ValidateAllProperties();
    }

    public void Delete()
    {
        _appointment.User?.Appointments.Remove(_appointment);
        _appointment.Customer.Appointments.Remove(_appointment);
        DeleteFromDb();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (!_initialized) return;
        if (e.PropertyName == nameof(HasErrors)) return;
        if (e.PropertyName == nameof(IsModified)) return;
        if (_initialized && e.PropertyName != nameof(IsModified)) IsModified = true;
        SaveCommand.NotifyCanExecuteChanged();
    }

    protected override void SaveEntity()
    {
        _appointment.type = Type;
        _appointment.start = Start.ToUniversalTime();
        _appointment.end = End.ToUniversalTime();
        _appointment.Customer = Customer;
        _appointment.User = User;
        if (!_appointment.User!.Appointments.Contains(_appointment)) _appointment.User.Appointments.Add(_appointment);
        if (!_appointment.Customer!.Appointments.Contains(_appointment)) _appointment.Customer.Appointments.Add(_appointment);
    }

    private bool CanSave() => !HasErrors;

    public bool IsInDb => _appointment.Id > 0;

    private DateTimeRange DateTimeRange => new(Start, End);

    private static bool CheckOpenHours(DateTime time) => GetEstTime(time).IsBetween(new(9, 0), new(17, 1));

    private static bool CheckOpenDays(DateTime time) => DayOfWeek.Monday <= GetEstDate(time).DayOfWeek && GetEstDate(time).DayOfWeek <= DayOfWeek.Friday;

    private static DateOnly GetEstDate(DateTime time)
    {
        var convertedDateTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        return DateOnly.FromDateTime(convertedDateTime);
    }
    private static TimeOnly GetEstTime(DateTime time)
    {
        var convertedDateTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        return TimeOnly.FromDateTime(convertedDateTime);
    }

    private static bool IsOverlapping(AppointmentDtoVm appointment, DateTime start, DateTime end)
    {
        return DateTimeRange.IsOverlapping(appointment.DateTimeRange, new(start, end));
    }

    private bool IsOverlappable(AppointmentDtoVm apt) => apt.User.Id == User.Id;

    private IEnumerable<AppointmentDtoVm> GetOverlappable() => Appointments
         .Except(Overlaps.Append(this))
         .Where(a => IsOverlappable(a));

    private void AddOverlap(AppointmentDtoVm appointment)
    {
        if (!appointment.Overlaps.Contains(this)) appointment.Overlaps.Add(this);
        if (!Overlaps.Contains(appointment)) Overlaps.Add(appointment);
    }

    private void RemoveOverlap(AppointmentDtoVm appointment)
    {
        appointment.Overlaps.Remove(this);
        Overlaps.Remove(appointment);
    }

    private void CheckOverlaps(DateTime start, DateTime end)
    {
        //clean out old overlaps
        foreach (var apt in Overlaps.ToArray())
        {
            if (!IsOverlappable(apt)) RemoveOverlap(apt);
            if (!IsOverlapping(apt, start, end)) RemoveOverlap(apt);
        }

        //check for new overlaps
        var overlaps = GetOverlappable().Where(a => IsOverlapping(a, start, end));
        foreach (var overlap in overlaps)
        {
            AddOverlap(overlap);
        }
    }
    private void ValidateTimes()
    {
        ValidateProperty(End, nameof(End));
        ValidateProperty(Start, nameof(Start));
    }
    private void CheckAppointmentDates(DateTime oldDate, DateTime newDate)
    {
        if (oldDate.Date == newDate.Date) return;
        var message = new AppointmentDateChangedMessage(DateOnly.FromDateTime(oldDate), DateOnly.FromDateTime(newDate));
        Messenger.Send(message);
    }

    private string GetFormattedDateTime(DateTime dateTime)
    {
        var date = dateTime.ToString("d", CultureInfo.CurrentCulture);
        var time = dateTime.ToShortTimeString();
        return $"{date} {time}";
    }

}