using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

public class DateBoundDatePicker : DatePicker
{
    public static readonly DependencyProperty AvailableDatesProperty =
        DependencyProperty.Register("AvailableDates", typeof(IEnumerable<DateOnly>), typeof(DateBoundDatePicker), new PropertyMetadata(null, OnAvailableDatesChanged));

    static DateBoundDatePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DateBoundDatePicker), new FrameworkPropertyMetadata(typeof(DateBoundDatePicker)));
    }

    public ICollection<DateTime> AvailableDates
    {
        get { return (ICollection<DateTime>)GetValue(AvailableDatesProperty); }
        set { SetValue(AvailableDatesProperty, value); }
    }


    private static void OnAvailableDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cast = (DateBoundDatePicker)d;
        if (cast.AvailableDates.Count < 1) return;
        var dates = cast.AvailableDates.OrderBy(d => d).ToArray();

        //clear current calendar to prevent OutOfRange exceptions
        cast.SelectedDate = null;
        cast.DisplayDateStart = null;
        cast.DisplayDateEnd = null;
        cast.BlackoutDates.Clear();

        //set new calendar display bounds
        cast.DisplayDateStart = dates.First();
        cast.DisplayDateEnd = dates.Last();

        //set new blackout dates
        for (int i = 0; i < dates.Length - 1; i++)
        {
            var rangeStart = dates[i].AddDays(1);
            var next = dates[i + 1];
            if (rangeStart != next) cast.BlackoutDates.Add(new(rangeStart, next.AddDays(-1)));
        }
    }
}