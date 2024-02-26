using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;
using System.Windows;

namespace AppointmentsManager.WpfApp.Mvvm.Views;
public class WindowBase : Window
{
    public WindowBase(WindowVmBase vm)
    {
        DataContext = vm;
        vm.CloseAction = () => Close();
    }
}
