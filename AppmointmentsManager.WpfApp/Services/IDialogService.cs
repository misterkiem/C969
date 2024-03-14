
namespace AppointmentsManager.WpfApp.Services;

public interface IDialogService
{
    bool ShowConfirmMessage(string message, string title);
    void ShowExceptionMessage(string message, string title, Exception ex);
    void ShowMessage(string message, string title);
}