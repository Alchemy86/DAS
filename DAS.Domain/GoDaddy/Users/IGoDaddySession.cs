using DAS.Domain.DeathbyCapture;

namespace DAS.Domain.GoDaddy.Users
{
    public interface IGoDaddySession
    {
        bool LoggedIn { get; }

        string Username { get; }

        string Password { get; }

        void SetLogin(bool loggedIn);

        GoDaddyAccount GoDaddyAccount { get; }

        DeathByCaptureDetails DeathByCapture { get; }
    }
}