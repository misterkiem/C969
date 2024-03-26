using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.Messages;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Windows.Threading;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

public enum AppointmentFilter
{
    UserAppointments,

    All,

    ByDay
}

[ObservableRecipient]
public partial class AppointmentManagerControlVm : ControlVmBase,
    IRecipient<AppointmentDateChangedMessage>,
    IRecipient<AppointmentSavedMessage>,
    IRecipient<NewAppointmentDiscardedMessage>,
    IRecipient<AppointmentsDeletedMessage>
{
    private readonly IDtoVmFactory<AppointmentDtoVm> _appointmentFac;

    private readonly ILoginService _login;

    private readonly IDialogService _dialog;

    private readonly IDataService _data;

    private ObservableCollection<AppointmentDtoVm> _appointments;

    public AppointmentManagerControlVm(IDataService dataService,
                                IDtoVmFactory<AppointmentDtoVm> appointmentFac,
        IMessenger messenger,
        ILoginService login,
        IDialogService dialog)
    {
        _data = dataService;
        Messenger = messenger;
        _login = login;
        _dialog = dialog;
        _appointmentFac = appointmentFac;

        _appointments = new(_data.Appointments.Select(x => _appointmentFac.CreateFromExisting(x)));
        foreach (var appointment in _appointments) appointment.Appointments = _appointments;
        AppointmentsView = CollectionViewSource.GetDefaultView(_appointments);
        AppointmentsView.Filter = (x) => FilterAppointment((AppointmentDtoVm)x);
        SortDescription sort = new(nameof(AppointmentDtoVm.Start), ListSortDirection.Ascending);
        AppointmentsView.SortDescriptions.Add(sort);

        AvailableDates = new(_appointments.SelectMany((x) => x.AppointmentDates).Distinct());
        Messenger.RegisterAll(this);
    }

    [ObservableProperty]
    private ICollectionView? _appointmentsView;

    [ObservableProperty]
    private DateOnly? _selectedDate;


    [ObservableProperty]
    private ObservableCollection<DateOnly> _availableDates;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteAppointmentCommand))]
    [NotifyCanExecuteChangedFor(nameof(NewAppointmentCommand))]
    private AppointmentDtoVm? _selectedAppointment;

    private AppointmentFilter _selectedFilter;

    public AppointmentFilter SelectedFilter
    {
        get { return _selectedFilter; }
        set
        {
            TryChangeFilter(_selectedFilter, value);
        }
    }


    [RelayCommand]
    private void NewAppointment()
    {
        if (SelectedAppointment?.IsModified ?? false)
        {
            OnCantChangeAppointment();
            return;
        }
        var newVm = _appointmentFac.CreateEmpty();
        newVm.Appointments = _appointments;
        _appointments.Add(newVm);
        SelectedAppointment = newVm;
        SelectedAppointment.CheckOverlaps();
    }


    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void DeleteAppointment()
    {

        if (SelectedAppointment is null) return;
        SelectedAppointment.Delete();
        _appointments.Remove(SelectedAppointment);
    }
    private bool CanDelete() => SelectedAppointment?.DbModel.Id > 0;

    public void Receive(AppointmentDateChangedMessage message)
    {
        var oldDate = message.OldValue;
        var newDate = message.NewValue;
        if (!_appointments.Any((x) => x.IsOnDate(oldDate))) AvailableDates.Remove(oldDate);
        if (!AvailableDates.Any((x) => x == newDate)) AvailableDates.Add(newDate);
    } 

    public void Receive(AppointmentSavedMessage message)
    {
        DeleteAppointmentCommand.NotifyCanExecuteChanged();
    }
    public void Receive(NewAppointmentDiscardedMessage message)
    {
        _appointments.Remove(message.Appointment);
    }

    public void Receive(AppointmentsDeletedMessage message)
    {
        foreach (var apt in message.Appointments)
        {
            var vm = _appointments.Where(x => x.DbModel == apt).FirstOrDefault(); ;
            if (vm is null) continue;
            _appointments.Remove(vm);
            if (vm == SelectedAppointment) SelectedAppointment = null;
        }
    }


    void TryChangeFilter(AppointmentFilter oldValue, AppointmentFilter newValue)
    {
        if (SelectedAppointment?.IsModified ?? false)
        {
            OnCantChangeAppointment();
            return;
        }
        SelectedAppointment = null;
        _selectedFilter = newValue;
        OnPropertyChanged(nameof(SelectedFilter));
        AppointmentsView?.Refresh();
    }

    partial void OnSelectedDateChanged(DateOnly? value) => AppointmentsView?.Refresh();

    partial void OnSelectedAppointmentChanged(AppointmentDtoVm? oldValue, AppointmentDtoVm? newValue)
    {
        if (CheckForAppointmentChange(oldValue)) return;
        oldValue!.ValidateProperties();
        Dispatcher.CurrentDispatcher.BeginInvoke(() => SelectedAppointment = oldValue);
    }

    public AppointmentDtoVm? old_value;

    private bool FilterAppointment(AppointmentDtoVm appointment)
    {
        switch (SelectedFilter)
        {
            case AppointmentFilter.All:
                return true;

            case AppointmentFilter.ByDay:
                if (SelectedDate is null) return false;
                if (DateOnly.FromDateTime(appointment.Start) == SelectedDate) return true;
                if (DateOnly.FromDateTime(appointment.End) == SelectedDate) return true;
                return false;

            case AppointmentFilter.UserAppointments:
                return appointment.User?.userId == _login.LoggedInUser?.userId;

            default:
                return true;
        }
    }

    private bool CheckForAppointmentChange(AppointmentDtoVm? appointment)
    {
        if (appointment is null) return true;
        if (!appointment.IsModified) return true;
        OnCantChangeAppointment();
        return false;
    }
    private void OnCantChangeAppointment()
    {
        _dialog.ShowMessage("Current appointment has unsaved changes. Save or cancel changes before selecting or adding new appointments.",
            "Must Finalize Appointment");
    }

    private ValidationResult? CheckAppointment(AppointmentDtoVm appointment)
    {
        if (_appointments.Any(x => x.Start < appointment.End || x.End > appointment.Start))
            return new("This appointment is overlapping with an existing appointment.");
        return ValidationResult.Success;
    }

}