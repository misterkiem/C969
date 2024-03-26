using AppointmentsManager.WpfApp.Core;
using AppointmentsManager.WpfApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.WindowVms;

public partial class LoginWindowVm : WindowVmBase
{
    private static readonly HashSet<string> _spanishRegions = new()
    {
        "AR", // Argentina
        "BO", // Bolivia
        "CL", // Chile
        "CO", // Colombia
        "CR", // Costa Rica
        "CU", // Cuba
        "DO", // Dominican Republic
        "EC", // Ecuador
        "SV", // El Salvador
        "GQ", // Equatorial Guinea
        "GT", // Guatemala
        "HN", // Honduras
        "MX", // Mexico
        "NI", // Nicaragua
        "PA", // Panama
        "PY", // Paraguay
        "PR", // Puerto Rico
        "PE", // Peru
        "ES", // Spain
        "UY", // Uruguay
        "VE"  // Venezuela
    };


    private readonly IDataService _data = null!;
    private readonly INavService _windowManager = null!;
    private readonly ILoginService _login = null!;
    private readonly IDialogService _dialog;
    private RegionInfo _region = RegionInfo.CurrentRegion;



    [Obsolete("Design Time Only", true)]
    public LoginWindowVm() { }

    public LoginWindowVm(IDataService data, INavService WindowManager, ILoginService login, IDialogService dialog)
    {
        _data = data;
        _windowManager = WindowManager;
        _login = login;
        _dialog = dialog;

        if (_spanishRegions.Contains(_region.TwoLetterISORegionName)) InitSpanish();
        else InitEnglish();
    }

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string _usernameLabel = "Username";

    [ObservableProperty]
    private string _passwordLabel = "Password";

    [ObservableProperty]
    private string _message = "Please enter your username and password.";

    [ObservableProperty]
    private string _regionMessage;

    [ObservableProperty]
    private string _loginText = "Login";

    [ObservableProperty]
    private string _loginErrorText = "Invalid username and password combination.";

    [ObservableProperty]
    private bool _invalidLogin = false;

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

    void InitEnglish()
    {
        UsernameLabel = "Username";
        PasswordLabel = "Password";
        Message = "Please enter your username and password";
        LoginErrorText = "Invalid username and password combination.";
        LoginText = "Login";
        RegionMessage = $"Your Region: {_region.DisplayName}";
    }

    void InitSpanish()
    {
        UsernameLabel = "Nombre de usuario";
        PasswordLabel = "Contraseña";
        Message = "Porfavor introduzca su nombre de usuario y contraseña";
        LoginErrorText = "Combinación de nombre de usuario y contraseña no válida.";
        LoginText = "Acceso";
        RegionMessage = $"Tu región: {_region.DisplayName}";
    }
}