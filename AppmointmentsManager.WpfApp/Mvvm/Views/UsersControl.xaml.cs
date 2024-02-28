using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for UsersControl.xaml
/// </summary>
public partial class UsersControl : ControlBase
{
    public UsersControl(UsersControlVm vm) : base(vm)
    {
        InitializeComponent();
    }
}