using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class CountryVm : DtoVmBase
{
    [ObservableProperty]
    private string? country;

    [ObservableProperty]
    private ObservableCollection<CityVm> cities = new();

    partial void OnCitiesChanged(ObservableCollection<CityVm> value)
        => SetView(value);

    public CountryVm() { }
}