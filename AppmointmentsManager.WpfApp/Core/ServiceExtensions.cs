using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentsManager.WpfApp.Core;
public static class ServiceExtensions
{
    public static void AddFactory<T>(this IServiceCollection services)
        where T : class
    {
        services.AddTransient<T>();
        services.AddSingleton<Func<T>>(x => () => x.GetService<T>()!);
        services.AddSingleton<IFactory<T>, Factory<T>>();
    }
}
