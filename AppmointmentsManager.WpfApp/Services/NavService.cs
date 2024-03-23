using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Services;

public partial class NavService : ObservableObject, INavService
{
    private readonly Dictionary<WindowType, Func<WindowBase>> _factories = new() { };

    public NavService(IFactory<MainWindow> mainWindowFactory, IFactory<LoginWindow> loginWindowFactory)
    {
        _factories.Add(WindowType.MainWindow, () => mainWindowFactory.Create());
        _factories.Add(WindowType.LoginWindow, () => loginWindowFactory.Create());
    }

    public void OpenWindow(WindowType windowType) => _factories[windowType].Invoke().Show();

    public void OpenDialog(WindowType windowType) => _factories[windowType].Invoke().ShowDialog();

    [ObservableProperty]
    ControlVmBase? currentView;
}