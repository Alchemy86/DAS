using DAS.Domain.Users;

namespace DAS.ServiceCall
{
    public interface IServiceCalls
    {
        bool LoginWP(string username, string password);

        void SendEmail(string username, string message);
    }
}
