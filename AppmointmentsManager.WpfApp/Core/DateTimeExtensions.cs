using System.Globalization;

namespace AppointmentsManager.WpfApp.Core;
public static class DateTimeExtensions
{
    public static string MonthName(this DateTime dateTime) => dateTime.ToString("MMMM", CultureInfo.CurrentCulture);
}
