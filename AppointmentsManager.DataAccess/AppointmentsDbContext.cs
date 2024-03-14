using AppointmentsManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;



namespace AppointmentsManager.DataAccess;

public class AppointmentsDbContext : DbContext
{
    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> optionsBuilder) : base(optionsBuilder) { }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<User> Users { get; set; }
}
