using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

public class DateBoundDatePicker : DatePicker
{
    static DateBoundDatePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DateBoundDatePicker), new FrameworkPropertyMetadata(typeof(DateBoundDatePicker)));
    }

    public ObservableCollection<DateOnly> AvailableDates
    {
        get { return (ObservableCollection<DateOnly>)GetValue(AvailableDatesProperty); }
        set { SetValue(AvailableDatesProperty, value); }
    }

    public static readonly DependencyProperty AvailableDatesProperty =
        DependencyProperty.Register("AvailableDates",
            typeof(ObservableCollection<DateOnly>),
            typeof(DateBoundDatePicker),
            new PropertyMetadata(null, OnAvailableDatesChanged));



    public DateOnly? SelectedDateOnly
    {
        get { return (DateOnly?)GetValue(SelectedDateOnlyProperty); }
        set { SetValue(SelectedDateOnlyProperty, value); }
    }

    public static readonly DependencyProperty SelectedDateOnlyProperty =
        DependencyProperty.Register("SelectedDateOnly",
            typeof(DateOnly?),
            typeof(DateBoundDatePicker),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedDateOnlyChanged));

    private static void OnSelectedDateOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DateBoundDatePicker cast) return;
        if (e.NewValue is not DateOnly newDate) return;
        cast.SelectedDate = newDate.ToDateTime(new());
    }

    private static void OnAvailableDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DateBoundDatePicker cast) return;
        if (cast.AvailableDates is null) return;
        var oldCollection = (ObservableCollection<DateOnly>)e.OldValue;
        var newCollection = (ObservableCollection<DateOnly>)e.NewValue;
        if (oldCollection is not null) oldCollection.CollectionChanged -= cast.OnDateCollectionChanged;
        if (newCollection is not null) newCollection.CollectionChanged += cast.OnDateCollectionChanged;
        cast.BuildBlackoutDates();
    }

    private void OnDateCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => BuildBlackoutDates();

    protected override void OnSelectedDateChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectedDateChanged(e);
        if (SelectedDate is null) return;
        SelectedDateOnly = DateOnly.FromDateTime(SelectedDate.Value);
    }

    void BuildBlackoutDates()
    {
        if (AvailableDates is null) return;
        BlackoutDates.Clear();
        if (AvailableDates.Count < 1) return;
        SelectedDate = null;
        DisplayDateStart = null;
        DisplayDateEnd = null;
        BlackoutDates.Clear();

        var dates = AvailableDates.OrderBy(d => d).ToArray();
        //set new calendar display bounds
        DisplayDateStart = dates.First().ToDateTime(new());
        DisplayDateEnd = dates.Last().ToDateTime(new());

        //set new blackout dates
        for (int i = 0; i < dates.Length - 1; i++)
        {
            var rangeStart = dates[i].AddDays(1).ToDateTime(new());
            var next = dates[i + 1].ToDateTime(new());
            if (rangeStart != next)
            {
                BlackoutDates.Add(new(rangeStart, next.AddDays(-1)));
            }
        }
    }
}