using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services;

public interface IDataService
{
    ObservableCollection<AddressVm> Addresses { get; }

    ObservableCollection<AppointmentVm> Appointments { get; }

    ObservableCollection<CityVm> Cities { get; }

    ObservableCollection<CountryVm> Countries { get; }

    ObservableCollection<CustomerVm> Customers { get; }

    ObservableCollection<UserVm> Users { get; }
}