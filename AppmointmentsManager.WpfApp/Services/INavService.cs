using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

namespace AppointmentsManager.WpfApp.Services;

public interface INavService
{
    void OpenDialog(WindowType windowType);

    void OpenWindow(WindowType windowType);

    public ControlVmBase CurrentView { get; set; }
}