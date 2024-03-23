using AppointmentsManager.DataAccess.Models;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services;

public interface IDataService
{
    ReadOnlyObservableCollection<Address> Addresses { get; }
    ReadOnlyObservableCollection<Appointment> Appointments { get; }
    ReadOnlyObservableCollection<City> Cities { get; }
    ReadOnlyObservableCollection<Country> Countries { get; }
    ReadOnlyObservableCollection<Customer> Customers { get; }
    ReadOnlyObservableCollection<User> Users { get; }

    void DeleteModel(DbModel model);
    void SaveModel(DbModel model);
    void SaveModel(DbModel model, IEnumerable<DbModel> dependents);
}