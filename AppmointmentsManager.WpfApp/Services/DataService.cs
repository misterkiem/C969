using AppointmentsManager.DataAccess;
using AppointmentsManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services
{
    public class DataService : IDataService
    {
        private readonly IDbContextFactory<AppointmentsDbContext> _dbFactory;

        private ObservableCollection<Appointment> appointments = null!;

        public ReadOnlyObservableCollection<Appointment> Appointments => new(appointments);

        private ObservableCollection<Country> countries = null!;

        public ReadOnlyObservableCollection<Country> Countries => new(countries);

        private ObservableCollection<User> users = null!;

        public ReadOnlyObservableCollection<User> Users => new(users);

        private ObservableCollection<Customer> customers = null!;

        public ReadOnlyObservableCollection<Customer> Customers => new(customers);

        private ObservableCollection<Address> addresses = null!;

        public ReadOnlyObservableCollection<Address> Addresses => new(addresses);

        private ObservableCollection<City> cities = null!;

        public ReadOnlyObservableCollection<City> Cities => new(cities);

        public DataService(IDbContextFactory<AppointmentsDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            InitCollections();
        }

        public void SaveModel(DbModel model)
        {
            using var context = _dbFactory.CreateDbContext();
            MarkModelForSave(model, context);
            context.SaveChanges();
        }

        public void SaveModel(DbModel model, IEnumerable<DbModel> dependents)
        {
            if (dependents.Any(x => x is null)) throw
                    new ArgumentException($"Missing data selections for this {model.GetType().Name}.");
            using var context = _dbFactory.CreateDbContext();
            MarkModelForSave(model, context);
            foreach (DbModel dependentEntity in dependents) MarkModelForSave(dependentEntity, context);
            context.SaveChanges();
        }

        public void DeleteModel(DbModel model)
        {
            if (model.Id == 0)
            {
                throw new ArgumentException("Customer has not been saved to database.");
            }
            using var context = _dbFactory.CreateDbContext();
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public bool CheckLogin(string username, string password)
        {
            var user = users.Where(u => u.userName == username).FirstOrDefault();
            if (user is null || user.password != password) return false;
            return true;
        }

        void MarkModelForSave(DbModel model, AppointmentsDbContext context)
        {
            context.Entry(model).State = model.Id == 0 ?
                EntityState.Added : EntityState.Modified;
        }

        void InitCollections()
        {
            using var db = _dbFactory.CreateDbContext();
            var dbCountries = db.Countries.AsNoTracking()
                .Include(c => c.Cities)
                .ThenInclude(c => c.Addresses)
                .ThenInclude(c => c.Customers)
                .ThenInclude(c => c.Appointments)
                .ThenInclude(a => a.User)
                .ToArray();

            countries = new(dbCountries);
            cities = new(countries.SelectMany(x => (x.Cities)));
            addresses = new(cities.SelectMany(x => (x.Addresses)));
            customers = new(addresses.SelectMany(x => (x.Customers)));
            appointments = new(customers.SelectMany(x => (x.Appointments)));
            var emptyUsers = db.Users.AsNoTracking().Include(u => u.Appointments).Where(u => u.Appointments.Count == 0).ToArray();
            var aptUsers = appointments.Select(x => x.User).DistinctBy(u => u.userId);
            users = new(aptUsers.Concat(emptyUsers));
            db.Dispose();
        }
    }
}