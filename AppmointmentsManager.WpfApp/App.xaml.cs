using AppointmentsManager.DataAccess;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;
using AppointmentsManager.WpfApp.Properties;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace AppointmentsManager.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) => InitServices(services))
                .Build();
        }

        void InitServices(IServiceCollection services)
        {

            services.AddDbContextFactory<AppointmentsDbContext>(opt =>
            {
                var connectionString = Settings.Default.client_schedule;
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            },
            ServiceLifetime.Transient);

            services.AddSingleton<IDataService, MySqlDataService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IMessenger>((provider) => WeakReferenceMessenger.Default);
            services.AddSingleton<INavService, NavService>();

            services.AddFactory<MainWindow>();
            services.AddFactory<LoginWindow>();
            services.AddFactory<ReportsControlVm>();

            services.AddDtoVmFactory<CustomerDtoVm>();
            services.AddDtoVmFactory<AppointmentDtoVm>();

            services.AddSingleton<MainWindowVm>();
            services.AddTransient<LoginWindowVm>();
            services.AddTransient<AppointmentManagerControlVm>();
            services.AddTransient<CustomerManagerControlVm>();
            services.AddTransient<ReportsControlVm>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var window = AppHost.Services.GetRequiredService<LoginWindow>();
            window.Show();
        }

    }
}

