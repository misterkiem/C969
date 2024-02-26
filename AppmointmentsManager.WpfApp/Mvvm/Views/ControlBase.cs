using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;
public abstract class ControlBase : UserControl
{
    public ControlBase(ControlVmBase vm)
    {
        DataContext = vm;
    }

}
