using AppointmentsManager.DataAccess;
using AppointmentsManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services
{
    public class MySqlDataService : IDataService
    {
        private readonly IDbContextFactory<AppointmentsDbContext> _dbFactory;

        private ObservableCollection<Country> _countries = null!;

        public ReadOnlyObservableCollection<Country> Countries { get; private set; } = null!;

        private ObservableCollection<City> _cities = null!;

        public ReadOnlyObservableCollection<City> Cities { get; private set; } = null!;

        private ObservableCollection<Address> _addresses = null!;

        public ReadOnlyObservableCollection<Address> Addresses { get; private set; } = null!;

        private ObservableCollection<Customer> _customers = null!;

        public ReadOnlyObservableCollection<Customer> Customers { get; private set; } = null!;

        private ObservableCollection<Appointment> _appointments = null!;

        public ReadOnlyObservableCollection<Appointment> Appointments { get; private set; } = null!;

        private ObservableCollection<User> _users = null!;

        public ReadOnlyObservableCollection<User> Users { get; private set; } = null!;

        private static Dictionary<Type, Func<MySqlDataService, IList>> _collectionMap = new()
        {
            { typeof(Country), (x) => x._countries },
            { typeof(City), (x) => x._cities },
            { typeof(Address), (x) => x._addresses },
            { typeof(Customer), (x) => x._customers },
            { typeof(Appointment), (x) => x._appointments },
            { typeof(User), (x) => x._users },
        }; 

        public MySqlDataService(IDbContextFactory<AppointmentsDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            InitCollections();
        }

        public void SaveModel(DbModel model)
        {
            using var context = _dbFactory.CreateDbContext();
            MarkModelForSave(model, context);
            context.SaveChanges();
            CheckForAdd(model);
        }

        public void SaveModel(DbModel model, IEnumerable<DbModel> dependents)
        {
            if (dependents.Any(x => x is null)) throw
                    new ArgumentException($"Missing data selections for this {model.GetType().Name}.");
            using var context = _dbFactory.CreateDbContext();
            MarkModelForSave(model, context);
            foreach (DbModel dependentEntity in dependents) MarkModelForSave(dependentEntity, context);
            context.SaveChanges();
            CheckForAdd(model);
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
            RemoveModel(model);
        }

        public bool CheckLogin(string username, string password)
        {
            var user = _users.Where(u => u.userName == username).FirstOrDefault();
            if (user is null || user.password != password) return false;
            return true;
        }

        void MarkModelForSave(DbModel model, AppointmentsDbContext context)
        {
            context.Entry(model).State = model.Id == 0 ?
                EntityState.Added : EntityState.Modified;
        } 

        private void CheckForAdd(DbModel model)
        {
            if (!_collectionMap.TryGetValue(model.Type, out var func)) return;
            if (func is null) return;
            var collection = func(this);
            if (!collection.Contains(model)) collection.Add(model);
        }

        private void RemoveModel(DbModel model)
        {
            if (!_collectionMap.TryGetValue(model.Type, out var func)) return;
            if (func is null) return;
            func(this).Remove(model);
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

            _countries = new(dbCountries);
            _cities = new(_countries.SelectMany(x => (x.Cities)));
            _addresses = new(_cities.SelectMany(x => (x.Addresses)));
            _customers = new(_addresses.SelectMany(x => (x.Customers)));
            _appointments = new(_customers.SelectMany(x => (x.Appointments)));
            var emptyUsers = db.Users.AsNoTracking().Include(u => u.Appointments).Where(u => u.Appointments.Count == 0).ToArray();
            var aptUsers = _appointments.Select(x => x.User).DistinctBy(u => u.userId);
            _users = new(aptUsers.Concat(emptyUsers));
            InitReadOnlyCollections();
        }

        void InitReadOnlyCollections()
        {
            Countries = new(_countries);
            Cities = new(_cities);
            Addresses = new(_addresses);
            Customers = new(_customers);
            Appointments = new(_appointments);
            Users = new(_users);
        }
    }
}