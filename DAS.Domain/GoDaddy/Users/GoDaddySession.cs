using DAS.Domain.DeathbyCapture;

namespace DAS.Domain.GoDaddy.Users
{
    public class GoDaddySession : IGoDaddySession
    {
        public bool LoggedIn { get; private set; }

        public string Username { get; }

        public string Password { get; }

        public GoDaddyAccount GoDaddyAccount { get; }

        public DeathByCaptureDetails DeathByCapture { get; }

        public void SetLogin(bool loggedIn)
        {
            LoggedIn = loggedIn;
        }

        public GoDaddySession(string username, string password, GoDaddyAccount goDaddyAccount, DeathByCaptureDetails deathByCapture)
        {
            Username = username;
            Password = password;
            GoDaddyAccount = goDaddyAccount;
            DeathByCapture = deathByCapture;
        }
    }
}
