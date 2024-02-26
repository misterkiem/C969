using AppointmentsManager.DataAccess;
using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Mvvm.Views;
using AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppointmentsManager.WpfApp.Mvvm.Vms;
[ObservableObject]
public partial class LoginWindowVm : WindowVmBase
{
    private readonly IDataService data = null!;
    private readonly IWindowManager windowManager = null!;

    [Obsolete("Design Time Only", true)]
    public LoginWindowVm() { }

    public LoginWindowVm(IDataService _data, IWindowManager _windowManager)
    {
        data = _data;
        windowManager = _windowManager;
        if (NotEnglish) ChangeToSpanish();
    }

    [ObservableProperty]
    string? username;

    [ObservableProperty]
    string? password;

    [ObservableProperty]
    string usernameLabel = "Username";

    [ObservableProperty]
    string passwordLabel = "Password";

    [ObservableProperty]
    string message = "Please enter your username and password.";

    [ObservableProperty]
    string loginText = "Login";

    [ObservableProperty]
    string loginErrorText = "Invalid username and password combination.";

    [ObservableProperty]
    bool invalidLogin = false;

    bool NotEnglish => CultureInfo.CurrentCulture.TwoLetterISOLanguageName != "en";


    [RelayCommand]
    void Login()
    {
        var user = data.Users.Where(u => u.userName == Username).FirstOrDefault();
        if (user is null || user.password != Password ) { InvalidLogin = true; return; }
        windowManager.OpenMainWindow();
        CloseAction?.Invoke();
    }

    void ChangeToSpanish()
    {
        UsernameLabel = "Nombre de usuario";
        PasswordLabel = "Contraseña";
        Message = "Porfavor introduzca su nombre de usuario y contraseña";
        LoginErrorText = "Combinación de nombre de usuario y contraseña no válida.";
        LoginText = "Acceso";
    }

}
