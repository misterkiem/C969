using System.Text;
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

    public void ShowExceptionMessage(string message, string title, Exception ex)
    {
        var builder = new StringBuilder();
        var innerException = ex.InnerException;
        builder.AppendLine(message);
        builder.AppendLine($"Exception: {ex.Message}");
        if (innerException is not null) builder.AppendLine($"Inner Exception: {innerException.Message}");
        ShowMessage(builder.ToString(), title);
    }
}