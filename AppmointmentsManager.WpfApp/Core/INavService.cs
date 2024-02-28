namespace AppointmentsManager.WpfApp.Core;

public interface INavService
{
    void OpenDialog(WindowType windowType);

    void OpenWindow(WindowType windowType);
}