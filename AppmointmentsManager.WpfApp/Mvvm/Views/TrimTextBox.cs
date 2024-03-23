using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

public class TrimTextBox : TextBox
{
    public TrimTextBox()
    {
        LostFocus += Trim;
    }

    private void Trim(object sender, RoutedEventArgs e)
    {
        var trimBox = sender as TrimTextBox;
        var trimmed = trimBox!.Text.Trim();
        trimBox.Text = trimmed == string.Empty ? null : trimmed;
    }
}