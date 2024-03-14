using AppointmentsManager.DataAccess.Models;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
public partial class AppointmentDtoVm : DtoVmBase
{
    private Appointment _appointment = null!;

    public override DbModel DbModel => _appointment;

    public AppointmentDtoVm(IDataService data, IDialogService dialog) : base(data, dialog) { }

    [ObservableProperty]
    string? _title;

    [ObservableProperty]
    string? _description;

    [ObservableProperty]
    string? _location;

    [ObservableProperty]
    string? _contact;

    [ObservableProperty]
    string? _type;

    [ObservableProperty]
    string? _url;

    [ObservableProperty]
    DateTime? _start;

    [ObservableProperty]
    DateTime? _end;

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

    }

    protected override void SaveEntity()
    {
        _appointment.title = Title;
        _appointment.description = Description;
        _appointment.location = Location;
        _appointment.contact = Contact;
        _appointment.type = Type;
        _appointment.url = Url;
    }
}
