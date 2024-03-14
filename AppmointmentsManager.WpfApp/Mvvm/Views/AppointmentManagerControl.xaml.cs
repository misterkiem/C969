using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

public partial class AppointmentManagerControl : ControlBase
{
    public AppointmentManagerControl()
    {
        InitializeComponent(); 
        var test = new DatePicker();
        test.CalendarOpened += somethingNew;
    }

    private void somethingNew(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}