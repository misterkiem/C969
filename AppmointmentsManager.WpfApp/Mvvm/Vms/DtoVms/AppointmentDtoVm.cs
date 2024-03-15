using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class AppointmentDtoVm : DtoVmBase
{
    private Appointment _appointment = null!;

    public override DbModel DbModel => _appointment;

    public AppointmentDtoVm(IDataService data, IDialogService dialog) : base(data, dialog) { }

    [ObservableProperty]
    private string? _title;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private string? _location;

    [ObservableProperty]
    private string? _contact;

    [ObservableProperty]
    private string? _type;

    [ObservableProperty]
    private string? _url;

    [ObservableProperty]
    private DateTime _start;

    [ObservableProperty]
    private DateTime _end;

    [ObservableProperty]
    private Customer? _customer;

    [ObservableProperty]
    private User? _user;

    public override void InitEmpty()
    {
        _appointment = new();
    }

    public override void LoadEntity(DbModel entity)
    {
        if (entity is not Appointment appointment) return;
        base.LoadEntity(entity);
        _appointment = appointment;
        Title = _appointment.title;
        Description = _appointment.description;
        Location = _appointment.location;
        Contact = _appointment.contact;
        Type = _appointment.type;
        Url = _appointment.url;
        Start = TimeZoneInfo.ConvertTimeFromUtc(_appointment.start, TimeZoneInfo.Local);
        End = TimeZoneInfo.ConvertTimeFromUtc(_appointment.end, TimeZoneInfo.Local);
        Customer = _appointment.Customer;
        User = _appointment.User;
    }

    protected override void SaveEntity()
    {
        _appointment.title = Title;
        _appointment.description = Description;
        _appointment.location = Location;
        _appointment.contact = Contact;
        _appointment.type = Type;
        _appointment.url = Url;
        _appointment.start = TimeZoneInfo.ConvertTimeToUtc(Start);
        _appointment.end = TimeZoneInfo.ConvertTimeToUtc(End);
        _appointment.Customer = Customer;
        _appointment.User = User;
    }
}