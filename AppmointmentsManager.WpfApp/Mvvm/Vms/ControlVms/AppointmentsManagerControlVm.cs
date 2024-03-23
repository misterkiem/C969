﻿using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.Messages;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

public enum AppointmentFilter
{
    UserAppointments,

    All,

    ByDay
}

[ObservableRecipient]
public partial class AppointmentManagerControlVm : ControlVmBase, IRecipient<AppointmentDateChangedMessage>, IRecipient<AppointmentErrorsChanged>
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
        SortDescription sort = new(nameof(AppointmentDtoVm.Start), ListSortDirection.Ascending);
        AppointmentsView.SortDescriptions.Add(sort);

        AvailableDates = new(_appointments.SelectMany((x) => x.AppointmentDates).Distinct());
        Messenger.RegisterAll(this);
    }

    [ObservableProperty]
    private ICollectionView? _appointmentsView;

    private IDataService _data;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveAppointmentCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteAppointmentCommand))]
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
        var newVm = _appointmentFac.CreateEmpty();
        newVm.Appointments = _appointments;
        var user = _login.LoggedInUser;
        if (user is not null) newVm.User = user;
        _appointments.Add(newVm);
        SelectedAppointment = newVm;
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void SaveAppointment()
    {
        if (SelectedAppointment is null) return;
        SelectedAppointment.Save();
    }

    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void DeleteAppointment()
    {
        if (SelectedAppointment is null) return;
        SelectedAppointment.Delete();
        _appointments.Remove(SelectedAppointment); 
    }

    private bool CanDelete() => SelectedAppointment is not null;

    public void Receive(AppointmentDateChangedMessage message)
    {
        var oldDate = message.OldValue;
        var newDate = message.NewValue;
        if (!_appointments.Any((x) => x.IsOnDate(oldDate))) AvailableDates.Remove(oldDate);
        if (!AvailableDates.Any((x) => x == newDate)) AvailableDates.Add(newDate);
    }

    partial void OnSelectedFilterChanged(AppointmentFilter value)
    {
        SelectedAppointment = null;
        AppointmentsView?.Refresh();
    }

    partial void OnSelectedDateChanged(DateOnly? value) => AppointmentsView?.Refresh();

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

    private bool CanSave() => !SelectedAppointment?.HasErrors ?? false;

    private ValidationResult? CheckAppointment(AppointmentDtoVm appointment)
    {
        if (_appointments.Any(x => x.Start < appointment.End || x.End > appointment.Start))
            return new("This appointment is overlapping with an existing appointment.");
        return ValidationResult.Success;
    }

    public void Receive(AppointmentErrorsChanged message)
    {
        if (message.Appointment == SelectedAppointment) SaveAppointmentCommand.NotifyCanExecuteChanged();
    }
}