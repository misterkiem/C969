namespace AppointmentsManager.WpfApp.Core;
public struct DateTimeRange
{
    private DateTime Start { get; }
    private DateTime End { get; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    } 

    public bool IsOverlapping(DateTimeRange range)
    {
        if (range.Start < Start && Start < range.End) return true;
        if (range.Start < End && End < range.End) return true;
        return false;
    }

    public static bool IsOverlapping(DateTimeRange first, DateTimeRange second) => first.IsOverlapping(second) || second.IsOverlapping(first); 
}
