using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class CityVm : DtoVmBase
{
    [ObservableProperty]
    private string? city;

    [ObservableProperty]
    private CountryVm? country;

    [ObservableProperty]
    private ObservableCollection<AddressVm> addresses = new();

    partial void OnAddressesChanged(ObservableCollection<AddressVm> value)
        => SetView(value);

    public CityVm() { }

    public CityVm(IEnumerable<AddressVm> addresses) => Addresses = new(addresses);
}