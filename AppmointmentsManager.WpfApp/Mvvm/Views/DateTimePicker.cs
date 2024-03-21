using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

[ObservableObject]
public partial class DateTimePicker : DateBoundDatePicker
{
    public bool IsInternal = false;

    [ObservableProperty]
    private string _amPm = "AM";

    [ObservableProperty]
    private int _hour;

    [ObservableProperty]
    private int _minute;

    static DateTimePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimePicker), new FrameworkPropertyMetadata(typeof(DateTimePicker)));
    }

    public static int[] Hours { get; } = new[] { 12 }.Concat(Enumerable.Range(1, 11)).ToArray();

    public static int[] Minutes { get; } = Enumerable.Range(0, 59).ToArray();

    private int FullHour => AmPm == "AM" ? Hour : Hour + 12;



    public DateTime? SelectedDateTime
    {
        get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
        set { SetValue(SelectedDateTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectedDateTimeProperty =
        DependencyProperty.Register("SelectedDateTime",
            typeof(DateTime?),
            typeof(DateTimePicker),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedDateTimeChanged));

    private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cast = (DateTimePicker)d;
        if (cast.IsInternal) return;
        if (e.NewValue is null) return;
        var newDateTime = (DateTime)e.NewValue;

        cast.SelectedDate = newDateTime;
        cast.Minute = newDateTime.Minute;
        if (newDateTime.Hour > 12)
        {
            cast.Hour = newDateTime.Hour - 12;
            cast.AmPm = "PM";
        }
        else
        {
            cast.Hour = newDateTime.Hour;
            cast.AmPm = "AM";
        }
    }

    partial void OnAmPmChanged(string value) { SetDateTime(); }

    partial void OnHourChanged(int value) { SetDateTime(); }

    partial void OnMinuteChanged(int value) { SetDateTime(); }

    protected override void OnSelectedDateChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectedDateChanged(e);
        SetDateTime();
    }

    private void SetDateTime()
    {
        if (SelectedDate is null) return;
        var date = SelectedDate.Value;
        IsInternal = true;
        var newSelectedDate = new DateTime(date.Year, date.Month, date.Day, FullHour, Minute, 0);
        SelectedDateTime = newSelectedDate;
        IsInternal = false;
    }
}