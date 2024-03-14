using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

public partial class AppointmentManagerControlVm : ControlVmBase
{
    private IDataService _dataService; 

    [ObservableProperty]
    private ICollectionView? appointmentsView;

    public AppointmentManagerControlVm(IDataService dataService)
    {
        _dataService = dataService;
        AppointmentsView = CollectionViewSource.GetDefaultView(_dataService.Appointments);
    }
}