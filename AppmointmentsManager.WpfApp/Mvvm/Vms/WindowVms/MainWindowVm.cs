using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.Input;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public partial class MainWindowVm : WindowVmBase
{
    private readonly INavService _nav;

    private readonly AppointmentManagerControlVm _appointmentsCard;

    private readonly CustomerManagerControlVm _customersCard;

    private readonly ReportsControlVm _reportsVm;

    public MainWindowVm(INavService navService,
        AppointmentManagerControlVm appointmentVm,
        CustomerManagerControlVm customerVm,
        ReportsControlVm reportsVm)
    {
        _nav = navService;
        _appointmentsCard = appointmentVm;
        _customersCard = customerVm;
        _reportsVm = reportsVm;
    }

    public INavService Nav => _nav;

    [RelayCommand]
    private void NavToAppointments()
    {
        _nav.CurrentView = _appointmentsCard;
    }

    [RelayCommand]
    private void NavToCustomers()
    {
        _nav.CurrentView = _customersCard;
    }

    [RelayCommand]
    private void NavToReports()
    {
        _reportsVm.Refresh();
        _nav.CurrentView = _reportsVm;
    }
}