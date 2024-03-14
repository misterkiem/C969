﻿using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentsManager.WpfApp.Core;

public static class ServiceExtensions
{
    public static void AddFactory<T>(this IServiceCollection services)
        where T : class
    {
        services.AddTransient<T>();
        services.AddSingleton<Func<T>>((x) => () => x.GetService<T>()!);
        services.AddSingleton<IFactory<T>, Factory<T>>();
    }
    public static void AddCustomerCardVmFactory(this IServiceCollection services)
    {
        services.AddTransient<CustomerCardVm>();
        services.AddSingleton<Func<CustomerCardVm>>((x) => () => x.GetService<CustomerCardVm>()!);
        services.AddSingleton<ICustomerCardVmFactory, CustomerCardVmFactory>();
    }
}