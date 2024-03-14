using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

namespace AppointmentsManager.WpfApp.Core;

public interface INavService
{
    void OpenDialog(WindowType windowType);

    void OpenWindow(WindowType windowType);

    public ControlVmBase CurrentView { get; set; }
}