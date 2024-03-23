using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public partial class LoginWindowVm : WindowVmBase
{
    private readonly IDataService _data = null!;

    private readonly INavService _windowManager = null!;

    private readonly ILoginService _login = null!;
    private readonly IDialogService _dialog;

    [Obsolete("Design Time Only", true)]
    public LoginWindowVm() { }

    public LoginWindowVm(IDataService data, INavService WindowManager, ILoginService login, IDialogService dialog)
    {
        _data = data;
        _windowManager = WindowManager;
        _login = login;
        _dialog = dialog;
        if (NotEnglish) ChangeToSpanish();
    }

    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? password;

    [ObservableProperty]
    private string usernameLabel = "Username";

    [ObservableProperty]
    private string passwordLabel = "Password";

    [ObservableProperty]
    private string message = "Please enter your username and password.";

    [ObservableProperty]
    private string loginText = "Login";

    [ObservableProperty]
    private string loginErrorText = "Invalid username and password combination.";

    [ObservableProperty]
    private bool invalidLogin = false;

    bool NotEnglish => CultureInfo.CurrentCulture.TwoLetterISOLanguageName != "en";

    [RelayCommand]
    void Login()
    {
        if (Username is null || Password is null || !_login.AttemptLogin(Username, Password)) { InvalidLogin = true; return; }
        if (_data.Appointments.Any(apt =>
            apt.User == _login.LoggedInUser
            && DateTime.Now < apt.start.ToLocalTime()
            && apt.start.ToLocalTime() < DateTime.Now.AddMinutes(15)))
        {
            _dialog.ShowMessage("You have an upcoming appointment within the next 15 minutes!", "Upcoming Appointment");
        }
        _windowManager.OpenWindow(WindowType.MainWindow);
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