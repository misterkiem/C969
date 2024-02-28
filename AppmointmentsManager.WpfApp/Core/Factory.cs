namespace AppointmentsManager.WpfApp.Core;

public class Factory<T> : IFactory<T>
{
    protected readonly Func<T> _factory;

    public Factory(Func<T> factory) => _factory = factory;

    public T Create() => _factory();
}