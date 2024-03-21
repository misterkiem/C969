using AppointmentsManager.DataAccess.Models;

namespace AppointmentsManager.WpfApp.Services;
public interface ILoginService
{
    bool LoggedIn { get; }
    User? LoggedInUser { get; set; }

    bool AttemptLogin(string username, string password);
}