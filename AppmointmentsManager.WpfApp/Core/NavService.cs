using AppointmentsManager.WpfApp.Mvvm.Views;

namespace AppointmentsManager.WpfApp.Core;

public class NavService : INavService
{
    private readonly Dictionary<WindowType, Func<WindowBase>> factories = new() { };

    public NavService(IFactory<MainWindow> mainWindowFactory, IFactory<LoginWindow> loginWindowFactory)
    {
        factories.Add(WindowType.MainWindow, () => mainWindowFactory.Create());
        factories.Add(WindowType.LoginWindow, () => loginWindowFactory.Create());
    }

    public void OpenWindow(WindowType windowType) => factories[windowType].Invoke().Show();

    public void OpenDialog(WindowType windowType) => factories[windowType].Invoke().ShowDialog();
}