using AppointmentsManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;



namespace AppointmentsManager.DataAccess
{
    public class AppointmentsDbContext : DbContext
    {
        public static string ConnectionString { get; set; } = @"Host=localhost;Port=3306;Database=client_schedule;Username=sqlUser;Password=Passw0rd!";

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using var conn = new MySqlConnection(ConnectionString);
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(conn)); 
            base.OnConfiguring(optionsBuilder);
        }

    }
}
