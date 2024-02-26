using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using System.Windows;

namespace AppointmentsManager.WpfApp.Core;
public class WindowManager : IWindowManager
{
    readonly Dictionary<WindowType, Func<WindowBase>> factories = new() { };

    public WindowManager(IFactory<MainWindow> mainWindowFactory, IFactory<LoginWindow> loginWindowFactory)
    {
        factories.Add(WindowType.MainWindow, () => mainWindowFactory.Create());
        factories.Add(WindowType.LoginWindow, () => loginWindowFactory.Create());
    }


    public void OpenWindow(WindowType windowType) => factories[windowType].Invoke().Show();

    public void OpenDialog(WindowType windowType) => factories[windowType].Invoke().ShowDialog();
}
