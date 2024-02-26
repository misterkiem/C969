namespace AppointmentsManager.WpfApp.Core;

public interface IWindowManager
{
    void OpenDialog(WindowType windowType);
    void OpenWindow(WindowType windowType);
}