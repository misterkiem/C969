﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;

public partial class UserVm : DtoVmBase
{
    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private ObservableCollection<AppointmentVm> appointments = new();

    partial void OnAppointmentsChanged(ObservableCollection<AppointmentVm> value)
        => SetView(value);
}