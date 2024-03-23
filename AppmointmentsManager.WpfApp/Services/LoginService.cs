using AppointmentsManager.DataAccess.Models;
using System.IO;

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
            if (LoggedIn) LogLogin();
            return LoggedIn;
        }

        private void LogLogin()
        {
            var line = Enumerable.Repeat($"User {LoggedInUser!.userName} successfully logged in on {DateTime.Today:d} at {DateTime.Now:t}.", 1);
            var file = Path.Combine(Environment.CurrentDirectory, "Login_History.txt");
            File.AppendAllLines(file, line); 
        }
    }
}