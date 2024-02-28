using System.Windows;

namespace AppointmentsManager.WpfApp.Services;

public class DialogService : IDialogService
{
    public void ShowMessage(string message, string title) => MessageBox.Show(message, title);

    public bool ShowConfirmMessage(string message, string title)
    {
        var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
        if (result == MessageBoxResult.OK) return true;
        return false;
    }
}