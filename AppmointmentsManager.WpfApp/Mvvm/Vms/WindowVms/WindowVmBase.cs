using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public abstract class WindowVmBase : ObservableObject
{
    public Action? CloseAction { get; set; }
}