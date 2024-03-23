using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for TimePicker.xaml
/// </summary>
[ObservableObject]
public partial class DateTimePicker : UserControl
{
    public static int[] Hours { get; } = new[] { 12 }.Concat(Enumerable.Range(1, 11)).ToArray();

    public static int[] Minutes { get; } = Enumerable.Range(0, 60).ToArray();

    private bool _internalSet = false;

    private bool _externalSet = false;

    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Today;

    private DateOnly ToDateOnly => DateOnly.FromDateTime(SelectedDate);

    [ObservableProperty]
    private int _selectedHour = 12;

    [ObservableProperty]
    private int _selectedMinute = 0;

    [ObservableProperty]
    private string _selectedPeriod = "PM";

    public string Caption
    {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
    }

    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption",
            typeof(string),
            typeof(DateTimePicker),
            new PropertyMetadata(""));

    public DateTime SelectedDateTime
    {
        get { return (DateTime)GetValue(SelectedDateTimeProperty); }
        set { SetValue(SelectedDateTimeProperty, value); }
    }

    public static readonly DependencyProperty SelectedDateTimeProperty =
        DependencyProperty.Register("SelectedDateTime",
            typeof(DateTime),
            typeof(DateTimePicker),
            new FrameworkPropertyMetadata(new DateTime(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTimeChanged));

    partial void OnSelectedDateChanged(DateTime value) => SetTime();
    partial void OnSelectedHourChanged(int value) => SetTime();

    partial void OnSelectedMinuteChanged(int value) => SetTime();

    partial void OnSelectedPeriodChanged(string value) => SetTime();

    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var cast = (DateTimePicker)d;
        if (cast._internalSet) return;
        var newDateTime = (DateTime)e.NewValue;
        cast._externalSet = true;
        try { cast.SetInternalControls(newDateTime); }
        finally { cast._externalSet = false; }
    }

    private void SetInternalControls(DateTime newDateTime)
    {
        SelectedDate = newDateTime;
        SelectedMinute = newDateTime.Minute;
        SelectedPeriod = newDateTime.Hour < 12 ? "AM" : "PM";
        var adjustedHour = newDateTime.Hour % 12;
        SelectedHour = adjustedHour == 0 ? 12 : adjustedHour;
    }

    private void SetTime()
    {
        if (_externalSet) return;
        int newHour;
        if (SelectedHour == 12 && SelectedPeriod == "AM") newHour = 0;
        else if (SelectedHour != 12 && SelectedPeriod == "PM") newHour = SelectedHour + 12;
        else newHour = SelectedHour;
        var newTime = new TimeOnly(newHour, SelectedMinute);
        var newDateTime = ToDateOnly.ToDateTime(newTime);
        if (SelectedDateTime == newDateTime) return;
        _internalSet = true;
        try { SelectedDateTime = newDateTime; }
        finally { _internalSet = false; }
    }

    public DateTimePicker()
    {
        InitializeComponent();
    }
}