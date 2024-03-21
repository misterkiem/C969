using AppointmentsManager.DataAccess.Models;

namespace AppointmentsManager.WpfApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDataService _data;

        public LoginService(IDataService data)
        {
            _data = data;
        }

        public User? LoggedInUser { get; set; }

        public bool LoggedIn => LoggedInUser is not null;

        public bool AttemptLogin(string username, string password)
        {
            LoggedInUser = _data.Users.Where(x => x.userName == username && x.password == password).FirstOrDefault();
            return LoggedIn;
        }
    }
}