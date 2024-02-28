using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for AppointmentsControl.xaml
/// </summary>
public partial class AppointmentsControl : ControlBase
{
    public AppointmentsControl(AppointmentsControlVm vm) : base(vm)
    {
        InitializeComponent();
    }
}