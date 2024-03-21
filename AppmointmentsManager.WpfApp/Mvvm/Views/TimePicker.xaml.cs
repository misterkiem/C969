using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for TimePicker.xaml
/// </summary>
[ObservableObject]
public partial class TimePicker : UserControl
{
    public static int[] Hours { get; } = new[] { 12 }.Concat(Enumerable.Range(1, 11)).ToArray();

    public static int[] Minutes { get; } = Enumerable.Range(0, 60).ToArray();

    private bool _internalSet = false;

    [ObservableProperty]
    private int _hour = 12;

    [ObservableProperty]
    private int _minute = 0;

    [ObservableProperty]
    private string _period = "AM";

    public string Caption
    {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
    }

    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption",
            typeof(string),
            typeof(TimePicker),
            new PropertyMetadata(""));

    public TimeOnly Time
    {
        get { return (TimeOnly)GetValue(TimeProperty); }
        set { SetValue(TimeProperty, value); }
    }

    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("Time",
            typeof(TimeOnly),
            typeof(TimePicker),
            new FrameworkPropertyMetadata(new TimeOnly(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTimeChanged));

    partial void OnHourChanged(int value) => SetTime();

    partial void OnMinuteChanged(int value) => SetTime();

    partial void OnPeriodChanged(string value) => SetTime();

    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cast = (TimePicker)d;
        if (cast._internalSet) return;
        var newDateTime = (TimeOnly)e.NewValue;
        cast.Minute = newDateTime.Minute;
        cast.Period = newDateTime.Hour < 12 ? "AM" : "PM";
        var adjustedHour = newDateTime.Hour % 12;
        cast.Hour = adjustedHour == 0 ? 12 : adjustedHour;
    }

    private void SetTime()
    {
        int newHour;
        if (Hour == 12 && Period == "AM") newHour = 0;
        else if (Hour != 12 && Period == "PM") newHour = Hour + 12;
        else newHour = Hour;
        var newTime = new TimeOnly(newHour, Minute);
        if (Time == newTime) return;
        _internalSet = true;
        try { Time = newTime; }
        finally { _internalSet = false; }
    }

    public TimePicker()
    {
        InitializeComponent();
    }
}