using CommunityToolkit.Mvvm.ComponentModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class AppointmentVm : DtoVmBase
{
    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private string? location;

    [ObservableProperty]
    private string? contact;

    [ObservableProperty]
    private string? type;

    [ObservableProperty]
    private string? url;

    [ObservableProperty]
    private DateTime? start;

    [ObservableProperty]
    private DateTime? end;

    [ObservableProperty]
    private CustomerVm? customer;

    [ObservableProperty]
    private UserVm? user;
}