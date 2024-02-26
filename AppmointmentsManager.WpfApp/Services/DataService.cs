using AppointmentsManager.DataAccess;
using AppointmentsManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsManager.WpfApp.Services
{
    public class DataService : IDataService
    {
        public ObservableCollection<Appointment> Appointments { get; private set; }
        public ObservableCollection<Country> Countries { get; private set; }
        public ObservableCollection<User> Users { get; private set; }
        public ObservableCollection<Customer> Customers { get; private set; }
        public ObservableCollection<Address> Addresses { get; private set; }
        public ObservableCollection<City> Cities { get; private set; }

        public DataService(AppointmentsDbContext db)
        {
            Appointments = new ObservableCollection<Appointment>(db.Appointments);
            Countries = new ObservableCollection<Country>(db.Countries);
            Users = new ObservableCollection<User>(db.Users);
            Customers = new ObservableCollection<Customer>(db.Customers);
            Cities = new ObservableCollection<City>(db.Cities);
            Addresses = new ObservableCollection<Address>(db.Addresses);
        }

    }
}
