using AppointmentsManager.DataAccess;
using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Services
{
    public class DataService : IDataService
    {
        private readonly IDbContextFactory<AppointmentsDbContext> _dbFactory;

        public ObservableCollection<AppointmentVm> Appointments { get; private set; } = null!;

        public ObservableCollection<CountryVm> Countries { get; private set; } = null!;

        public ObservableCollection<UserVm> Users { get; private set; } = null!;

        public ObservableCollection<CustomerVm> Customers { get; private set; } = null!;

        public ObservableCollection<AddressVm> Addresses { get; private set; } = null!;

        public ObservableCollection<CityVm> Cities { get; private set; } = null!;

        public DataService(IDbContextFactory<AppointmentsDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
            InitCollections();
        }

        void InitCollections()
        {
            using var db = _dbFactory.CreateDbContext();
            var dbCountries = db.Countries.AsNoTracking()
                .Include(c => c.Cities)
                .ThenInclude(c => c.Addresses)
                .ThenInclude(c => c.Customers)
                .ThenInclude(c => c.Appointments)
                .ThenInclude(c => c.User)
                .ToArray();

            Countries = new(db.Countries.Select(x => GetCountryVm(x)));
            Cities = new(Countries.SelectMany(x => (x.Cities)));
            Addresses = new(Cities.SelectMany(x => (x.Addresses)));
            Customers = new(Addresses.SelectMany(x => (x.Customers)));
            Appointments = new(Customers.SelectMany(x => (x.Appointments)));
            Users = new(db.Users.Select(x => GetUserVm(x)));
        }

        CountryVm GetCountryVm(Country country)
        {
            var vm = new CountryVm()
            {
                Id = country.countryId,
                Country = country.country
            };
            vm.Cities = new(country.Cities.Select(x => GetCityVm(x, vm)));
            return vm;
        }

        CityVm GetCityVm(City city, CountryVm country)
        {
            var vm = new CityVm()
            {
                Id = city.cityId,
                City = city.city,
                Country = country
            };
            vm.Addresses = new(city.Addresses.Select(x => GetAddressVm(x, vm)));
            return vm;
        }

        AddressVm GetAddressVm(Address address, CityVm city)
        {
            var vm = new AddressVm()
            {
                Id = address.addressId,
                Address = address.address,
                Address2 = address.address2,
                PostalCode = address.postalCode,
                PhoneNumber = address.phone,
                City = city
            };
            vm.Customers = new(address.Customers.Select(x => GetCustomerVm(x, vm)));

            return vm;
        }

        CustomerVm GetCustomerVm(Customer customer, AddressVm address)
        {
            var vm = new CustomerVm()
            {
                Id = customer.customerId,
                CustomerName = customer.customerName,
                Address = address
            };
            vm.Appointments = new(customer.Appointments.Select(x => GetAppointmentVm(x, vm)));

            return vm;
        }

        AppointmentVm GetAppointmentVm(Appointment appointment, CustomerVm customer)
        {
            var vm = new AppointmentVm()
            {
                Id = appointment.appointmentId,
                Description = appointment.description,
                Location = appointment.location,
                Contact = appointment.contact,
                Type = appointment.type,
                Url = appointment.url,
                Start = appointment.start,
                End = appointment.end,
                Customer = customer
            };
            return vm;
        }

        UserVm GetUserVm(User user)
        {
            var vm = new UserVm()
            {
                Id = user.userId,
                Username = user.userName,
                Password = user.password
            };
            SetAppointmentsForUser(user, vm);
            return vm;
        }

        void SetAppointmentsForUser(User user, UserVm userVm)
        {
            userVm.Appointments = new(Appointments
                .Where(a => user.Appointments.Select(ua => ua.appointmentId)
                    .Contains(a.Id)));
        }
    }
}