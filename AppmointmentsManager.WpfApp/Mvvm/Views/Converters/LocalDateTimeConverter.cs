using System.Globalization;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Views.Converters;
public class LocalDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DateTime)value).ToLocalTime();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
