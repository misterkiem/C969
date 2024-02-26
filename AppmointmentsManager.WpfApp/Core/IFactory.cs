namespace AppointmentsManager.WpfApp.Core;

public interface IFactory<T>
{
    T Create();
}