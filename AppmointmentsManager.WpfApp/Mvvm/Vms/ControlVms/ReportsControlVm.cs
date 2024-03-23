using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms
{
    public partial class ReportsControlVm : ControlVmBase
    {
        private readonly IDataService _data;

        private readonly ReadOnlyObservableCollection<Appointment> _appointments;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserYearReport))]
        private int? _SelectedYear;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MonthTypeReport))]
        private string? _SelectedMonth;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MonthTypeReport))]
        private string? _selectedType;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserAppointments))]
        private User? _scheduleUser;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserYearReport))]
        private User? _yearUser;

        public IEnumerable<Appointment>? UserAppointments => ScheduleUser?.Appointments.OrderBy(a => a.start);

        public IEnumerable<int> Years => GetYears(_appointments);

        public IEnumerable<string> Months => GetMonths(_appointments);

        public IEnumerable<string> Types => _appointments.Select(x => x.type).Distinct();

        public string? MonthTypeReport => GetMonthTypeReport();

        public string? UserYearReport => GetUserYearReport();

        public ReadOnlyObservableCollection<User> Users { get; }

        public ReportsControlVm(IDataService data)
        {
            _data = data;
            _appointments = _data.Appointments;
            Users = _data.Users;
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(Months));
            OnPropertyChanged(nameof(Types));
            OnPropertyChanged(nameof(MonthTypeReport));
            OnPropertyChanged(nameof(UserAppointments));
        }

        private IEnumerable<string> GetMonths(IEnumerable<Appointment> appointments)
        {
            return appointments.Select(a => a.start.ToLocalTime().MonthName())
                .Concat(_appointments
                .Select(a => a.end.ToLocalTime().MonthName()))
                .Distinct();
        }

        private IEnumerable<int> GetYears(IEnumerable<Appointment> appointments)
        {
            return appointments.Select(a => a.start.ToLocalTime().Year)
                .Concat(_appointments
                .Select(a => a.end.ToLocalTime().Year))
                .Distinct();
        }

        private string? GetMonthTypeReport()
        {
            if (SelectedMonth is null || SelectedType is null) return null;
            var apts = _appointments
                .Where(a => a.start.ToLocalTime().MonthName() == SelectedMonth
                    || a.end.ToLocalTime().MonthName() == SelectedMonth)
                .Where(a => a.type == SelectedType);
            return $"There are {apts.Count()} appointments in {SelectedMonth} with appointment type {SelectedType}.";
        }

        private string? GetUserYearReport()
        {
            if (SelectedYear is null || YearUser is null) return null;
            var count = _appointments.Count(a => a.start.ToLocalTime().Year == SelectedYear
                || a.end.ToLocalTime().Year == SelectedYear);
            var hasHad = SelectedYear == DateTime.Now.Year ? "has" : "had";
            return $"The user {YearUser.userName} {hasHad} {count} appointments in the year {SelectedYear:d4}";
        }
    }
}