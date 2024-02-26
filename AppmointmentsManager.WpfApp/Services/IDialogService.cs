namespace AppointmentsManager.WpfApp.Services;

public interface IDialogService
{
    bool ShowConfirmMessage(string message, string title);
    void ShowMessage(string message, string title);
}