using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.ControlVms;

public partial class AppointmentManagerControlVm : ControlVmBase
{
    private IDataService _data;
    private readonly IDtoVmFactory<AppointmentDtoVm> _appointmentFac;
    private ObservableCollection<AppointmentDtoVm> _appointmentDtos;

    [ObservableProperty]
    private ICollectionView? appointmentsView;

    public AppointmentManagerControlVm(IDataService dataService, IDtoVmFactory<AppointmentDtoVm> appointmentFac)
    {
        _data = dataService;
        _appointmentFac = appointmentFac;
        _appointmentDtos = new(_data.Appointments.Select(x => _appointmentFac.CreateFromExisting(x)));
    }
}