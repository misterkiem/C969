using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentsManager.WpfApp.Core;
public static class ServiceExtensions
{
    public static void AddWindowFactory<TWindow>(this IServiceCollection services)
        where TWindow : WindowBase
    {
        services.AddTransient<TWindow>();
        services.AddSingleton<Func<TWindow>>(x => () => x.GetService<TWindow>()!);
        services.AddSingleton<IFactory<TWindow>, Factory<TWindow>>();
    }

    public static void AddVmFactory<TVm>(this IServiceCollection services)
        where TVm : ControlVmBase
    {
        services.AddTransient<TVm>();
        services.AddSingleton<Func<TVm>>(x => () => x.GetService<TVm>()!);
        services.AddSingleton<IFactory<TVm>, Factory<TVm>>();
    }
}
