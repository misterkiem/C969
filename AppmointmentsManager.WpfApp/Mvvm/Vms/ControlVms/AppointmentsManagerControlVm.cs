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

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

public enum AppointmentFilter
{
    UserAppointments,

    All,

    ByDay
}

[ObservableRecipient]
public partial class AppointmentManagerControlVm : ControlVmBase, IRecipient<AppointmentDateChangedMessage>
{
    private readonly IDtoVmFactory<AppointmentDtoVm> _appointmentFac;

    private readonly ILoginService _login;

    private ObservableCollection<AppointmentDtoVm> _appointments;

    public AppointmentManagerControlVm(IDataService dataService,
        IDtoVmFactory<AppointmentDtoVm> appointmentFac,
        IMessenger messenger,
        ILoginService login)
    {
        _data = dataService;
        Messenger = messenger;
        _login = login;
        _appointmentFac = appointmentFac;

        _appointments = new(_data.Appointments.Select(x => _appointmentFac.CreateFromExisting(x)));
        foreach (var appointment in _appointments) appointment.Appointments = _appointments;
        AppointmentsView = CollectionViewSource.GetDefaultView(_appointments);
        AppointmentsView.Filter = (x) => FilterAppointment((AppointmentDtoVm)x);

        AvailableDates = new(_appointments.Select((x) => x.Date).Distinct());
        Messenger.RegisterAll(this);
    }

    [ObservableProperty]
    private ICollectionView? _appointmentsView;

    private IDataService _data;

    [ObservableProperty]
    private AppointmentDtoVm? _selectedAppointment;

    [ObservableProperty]
    private DateOnly? _selectedDate;

    [ObservableProperty]
    private AppointmentFilter _selectedFilter = AppointmentFilter.UserAppointments;

    [ObservableProperty]
    private ObservableCollection<DateOnly> _availableDates;

    [RelayCommand]
    private void AddAppointment()
    {

    }

    [RelayCommand]
    private void SaveAppointment()
    {

    }

    [RelayCommand]
    private void DeleteAppointment()
    {

    } 

    public void Receive(AppointmentDateChangedMessage message)
    {
        var oldDate = message.oldValue;
        var newDate = message.newValue;
        if (!_appointments.Any((x) => x.Date == oldDate)) AvailableDates.Remove(oldDate);
        if (!AvailableDates.Any((x) => x == newDate)) AvailableDates.Add(newDate);
    }

    partial void OnSelectedFilterChanged(AppointmentFilter value) => AppointmentsView?.Refresh();

    partial void OnSelectedDateChanged(DateOnly? value) => AppointmentsView?.Refresh();

    private bool FilterAppointment(AppointmentDtoVm appointment)
    {
        switch (SelectedFilter)
        {
            case AppointmentFilter.All:
                return true;

            case AppointmentFilter.ByDay:
                if (SelectedDate is null) return false;
                return appointment.Date == SelectedDate;

            case AppointmentFilter.UserAppointments:
                return appointment.User?.userId == _login.LoggedInUser?.userId;

            default:
                return true;
        }
    }

    private ValidationResult? CheckAppointment(AppointmentDtoVm appointment)
    {
        if (_appointments.Any(x => x.Start < appointment.End || x.End > appointment.Start))
            return new("This appointment is overlapping with an existing appointment.");
        return ValidationResult.Success;
    }
}