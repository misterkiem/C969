using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.Messages;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

[ObservableRecipient]
public partial class AppointmentDtoVm : DtoVmBase
{
    private bool _initialized = false;

    private bool _isEndValidating = false;

    private bool _isStartValidating = false;

    private Appointment _appointment = null!;

    public AppointmentDtoVm(IDataService data,
        IDialogService dialog,
        IMessenger messenger) : base(data, dialog)
    {
        Messenger = messenger;
        Overlaps.CollectionChanged += (_, _) => ValidateTimes();
    }

    partial void OnDateChanged(DateOnly oldValue, DateOnly newValue)
    {
        Messenger.Send(new AppointmentDateChangedMessage(oldValue, newValue));
    }

    partial void OnEndChanged(TimeOnly value) => ValidateProperty(Start, nameof(Start));

    partial void OnEndChanging(TimeOnly value) => CheckNewEnd(value);

    partial void OnStartChanged(TimeOnly value) => ValidateProperty(End, nameof(End));

    partial void OnStartChanging(TimeOnly value) => CheckNewStart(value);

    public override DbModel DbModel => _appointment;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [Required]
    [NotifyDataErrorInfo]
    private Customer? _customer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [Required]
    [NotifyDataErrorInfo]
    private User _user = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [Required]
    [NotifyDataErrorInfo]
    private DateOnly _date;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(AppointmentDtoVm), nameof(ValidateTime))]
    [Required]
    private TimeOnly _end;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    [CustomValidation(typeof(AppointmentDtoVm), nameof(ValidateTime))]
    [Required]
    [NotifyDataErrorInfo]
    private TimeOnly _start;

    [ObservableProperty]
    [Required]
    [NotifyDataErrorInfo]
    private string? _type;

    public ObservableCollection<AppointmentDtoVm> Appointments { get; set; } = new();

    public ReadOnlyObservableCollection<Customer> Customers => _data.Customers;

    public ReadOnlyObservableCollection<User> Users => _data.Users;

    public string DisplayName => $"{Date}: {Start} - {End}, " +
        $"User: {User?.userName ?? "N/A"}, " +
        $"Customer: {Customer?.customerName ?? "N/A"}";

    private ObservableCollection<AppointmentDtoVm> Overlaps { get; } = new();

    public static ValidationResult? ValidateTime(object? value, ValidationContext context)
    {
        var instance = (AppointmentDtoVm)context.ObjectInstance;
        var time = (TimeOnly)value!;
        if (!instance.CheckOpenHours(time))
            return new($"Time is outside of bounds 9AM - 5PM Eastern Standard Time.");
        if (instance.Start > instance.End) return new("Start time is after end time.");
        if (instance.Overlaps.Count > 0)
            return new($"Times are overlapping with another appointment for this user");

        return ValidationResult.Success;
    }

    public override void InitEmpty()
    {
        _appointment = new()
        {
            title = string.Empty,
            description = string.Empty,
            location = string.Empty,
            contact = string.Empty,
            url = string.Empty,
        };
        Date = DateOnly.FromDateTime(DateTime.Today);
        Start = new(12, 0);
        End = Start.AddMinutes(30);
        _initialized = true;
    }

    public override void LoadEntity(DbModel entity)
    {
        if (entity is not Appointment appointment) return;
        base.LoadEntity(entity);
        _appointment = appointment;
        Type = _appointment.type;
        Date = DateOnly.FromDateTime(_appointment.start);
        Start = TimeOnly.FromDateTime(_appointment.start.ToLocalTime());
        End = TimeOnly.FromDateTime(_appointment.end.ToLocalTime());
        Customer = _appointment.Customer;
        User = _appointment.User;
        _initialized = true;
        ValidateAllProperties();
    }

    protected override void SaveEntity()
    {
        _appointment.type = Type;
        _appointment.start = Date.ToDateTime(Start);
        _appointment.end = Date.ToDateTime(End);
        _appointment.Customer = Customer;
        _appointment.User = User;
    }

    private bool CheckOpenHours(TimeOnly time) => ConvertEst(time).IsBetween(new(9, 0), new(17, 1));

    private TimeOnly ConvertEst(TimeOnly time)
    {
        var converted = TimeZoneInfo.ConvertTime(Date.ToDateTime(time), TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        return TimeOnly.FromDateTime(converted);
    }

    private void CheckNewEnd(TimeOnly newEnd)
    {
        //don't run if not initialized
        if (!_initialized) return;

        //clean out old overlaps
        foreach (var apt in Overlaps.ToArray())
        {
            if (!IsOverlappable(apt)) RemoveOverlap(apt);
            if (!OverlappingStart(apt, Start, newEnd)) RemoveOverlap(apt);
        }

        //check for new overlaps
        var overlaps = GetOverlappable().Where(a => OverlappingEnd(a, Start, newEnd));
        foreach (var overlap in overlaps) AddOverlap(overlap);
    }

    private void CheckNewStart(TimeOnly newStart)
    {
        //don't run if not initialized
        if (!_initialized) return;

        //clean out old overlaps
        foreach (var apt in Overlaps.ToArray())
        {
            if (!IsOverlappable(apt)) RemoveOverlap(apt);
            if (!OverlappingStart(apt, newStart, End)) RemoveOverlap(apt);
        }

        //check for new overlaps
        var overlaps = GetOverlappable().Where(a => OverlappingStart(a, newStart, End));
        foreach (var overlap in overlaps)
        {
            AddOverlap(overlap);
        }
    }

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

    private IEnumerable<AppointmentDtoVm> GetOverlappable() => Appointments
        .Except(Overlaps.Append(this))
        .Where(a => IsOverlappable(a));

    private bool IsCurrentlyOverlapping(AppointmentDtoVm appointment) =>
        IsOverlapping(appointment, Start, End);

    private bool IsOverlappable(AppointmentDtoVm apt) => apt.User.Id == User.Id && apt.Date == Date;

    private bool IsOverlapping(AppointmentDtoVm appointment, TimeOnly start, TimeOnly end)
        => OverlappingStart(appointment, start, end) || OverlappingEnd(appointment, start, end);


    private bool OverlappingEnd(AppointmentDtoVm appointment, TimeOnly start, TimeOnly end)
    {
        if (end == appointment.Start) return false;
        if (end.IsBetween(appointment.Start, appointment.End)) return true;
        if (appointment.End.IsBetween(start, end) && appointment.End != start) return true;
        return false;
    }

    private bool OverlappingStart(AppointmentDtoVm appointment, TimeOnly start, TimeOnly end)
    {
        if (start == appointment.End) return false;
        if (start.IsBetween(appointment.Start, appointment.End)) return true;
        if (appointment.Start.IsBetween(start, end) && appointment.Start != end) return true;
        return false;
    }

    private void ValidateTimes()
    {
        ValidateProperty(End, nameof(End));
        ValidateProperty(Start, nameof(Start));
    }
}