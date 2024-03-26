using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

namespace AppointmentsManager.WpfApp.Mvvm.Views;
/// <summary>
/// Interaction logic for AddAppointmentWindow.xaml
/// </summary>
public partial class AddAppointmentWindow : WindowBase
{
    public AddAppointmentWindow(AddAppointmentWindowVm vm) : base(vm)
    {
        InitializeComponent();
    }
}
