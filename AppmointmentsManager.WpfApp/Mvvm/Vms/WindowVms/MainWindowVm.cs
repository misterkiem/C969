using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Services;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public class MainWindowVm : WindowVmBase
{
    IDataService _dataService;
    public MainWindowVm(IDataService dataService) { _dataService = dataService; }

    public ObservableCollection<Appointment> Appointments => _dataService.Appointments;
}
