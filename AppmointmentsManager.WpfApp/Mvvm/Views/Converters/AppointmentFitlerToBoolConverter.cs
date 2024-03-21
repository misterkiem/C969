using AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;
using System.Globalization;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Views.Converters;
public class AppointmentFilterToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true? parameter : Binding.DoNothing;
    }
}
