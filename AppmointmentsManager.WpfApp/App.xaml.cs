using System.Windows;
using AppointmentsManager.WpfApp.Properties;
using AppointmentsManager.WpfApp.Services;
using AppointmentsManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppointmentsManager.WpfApp.Mvvm.Vms;
using AppointmentsManager.WpfApp.Mvvm.Views;
using Microsoft.Extensions.Hosting;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

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
                .ConfigureServices((hostContext, services) => InitServices(services))
                .Build();
        }
        void InitServices(IServiceCollection services)
        {

            services.AddDbContext<AppointmentsDbContext>(opt =>
            {
                var con = Settings.Default.client_schedule;
                opt.UseMySql(con, ServerVersion.AutoDetect(con));
            });
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<MainWindowVm>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddTransient<LoginWindowVm>();
            services.AddTransient<LoginWindow>();
            services.AddWindowFactory<MainWindow>();
            services.AddVmFactory<UsersControlVm>();
            services.AddVmFactory<AppointmentsControlVm>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            base.OnStartup(e);
            var window = AppHost.Services.GetRequiredService<LoginWindow>();
            window?.Show();
        }

    }
}

