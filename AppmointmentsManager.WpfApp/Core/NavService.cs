using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Core;

public partial class NavService : ObservableObject, INavService
{
    private readonly Dictionary<WindowType, Func<WindowBase>> factories = new() { };

    public NavService(IFactory<MainWindow> mainWindowFactory, IFactory<LoginWindow> loginWindowFactory)
    {
        factories.Add(WindowType.MainWindow, () => mainWindowFactory.Create());
        factories.Add(WindowType.LoginWindow, () => loginWindowFactory.Create());
    }

    public void OpenWindow(WindowType windowType) => factories[windowType].Invoke().Show();

    public void OpenDialog(WindowType windowType) => factories[windowType].Invoke().ShowDialog();

    [ObservableProperty]
    ControlVmBase? currentView;
}