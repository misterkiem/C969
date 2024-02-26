using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms;

namespace AppointmentsManager.WpfApp.Core;
public class Factory<T> : IFactory<T>
{
    private readonly Func<T> _factory;
    public Factory(Func<T> factory) => _factory = factory;
    public T Create() => _factory();
}
