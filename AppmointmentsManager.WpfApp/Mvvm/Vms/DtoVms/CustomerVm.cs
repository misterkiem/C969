using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class CustomerVm : DtoVmBase
{
    [ObservableProperty]
    private string customerName = string.Empty;

    [ObservableProperty]
    private AddressVm address = new();

    [ObservableProperty]
    private ObservableCollection<AppointmentVm> appointments = new();

    partial void OnAppointmentsChanged(ObservableCollection<AppointmentVm> value)
        => SetView(value);

    public CustomerVm() { }
}