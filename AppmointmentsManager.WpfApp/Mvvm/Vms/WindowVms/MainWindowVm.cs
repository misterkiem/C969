using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public partial class MainWindowVm : WindowVmBase
{
    private readonly INavService nav;

    private readonly AppointmentManagerControlVm appointmentsCard;

    private readonly CustomerManagerControlVm customersCard;

    public MainWindowVm(INavService navService,
        AppointmentManagerControlVm appointmentVm,
        CustomerManagerControlVm customerVm)
    {
        nav = navService;
        appointmentsCard = appointmentVm;
        customersCard = customerVm;
    }

    public INavService Nav => nav;

    [RelayCommand]
    void NavToAppointments()
    {
        nav.CurrentView = appointmentsCard;
    }

    [RelayCommand]
    void NavToCustomers()
    {
        nav.CurrentView = customersCard;
    }
}