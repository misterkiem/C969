using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class AddressVm : DtoVmBase
{
    [ObservableProperty]
    private string? address;

    [ObservableProperty]
    private string? address2;

    [ObservableProperty]
    private string? postalCode;

    [ObservableProperty]
    private string? phoneNumber;

    [ObservableProperty]
    private CityVm? city;

    [ObservableProperty]
    private ObservableCollection<CustomerVm> customers = new();

    partial void OnCustomersChanged(ObservableCollection<CustomerVm> value)
        => SetView(value);

    public AddressVm() { }
}