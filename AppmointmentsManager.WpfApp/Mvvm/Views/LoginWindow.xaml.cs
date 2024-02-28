﻿using AppointmentsManager.WpfApp.Mvvm.Vms;

namespace AppointmentsManager.WpfApp.Mvvm.Views;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : WindowBase
{
    public LoginWindow(LoginWindowVm vm) : base(vm)
    {
        InitializeComponent();
    }
}