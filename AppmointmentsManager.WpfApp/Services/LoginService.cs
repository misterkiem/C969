using AppointmentsManager.DataAccess.Models;
using System.IO;
using System.Text;

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
            LogLogin(username, password);
            return LoggedIn;
        }


        private void LogLogin(string username, string password)
        {
            string line;
            if (LoggedIn) line = $"User {LoggedInUser!.userName} successfully logged in";
            else line = $"Invalid login with username {username} and password {password}"; 
            line += $" on {DateTime.Today:d} at {DateTime.Now:t}."; 
            var file = Path.Combine(Environment.CurrentDirectory, "Login_History.txt");
            File.AppendAllLines(file, Enumerable.Repeat(line, 1));
        }
    }
}