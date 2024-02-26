using AppointmentsManager.DataAccess.Models;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services;
public interface IDataService
{
    ObservableCollection<Address> Addresses { get; }
    ObservableCollection<Appointment> Appointments { get; }
    ObservableCollection<City> Cities { get; }
    ObservableCollection<Country> Countries { get; }
    ObservableCollection<Customer> Customers { get; }
    ObservableCollection<User> Users { get; }
}