namespace DAS.ServiceCall
{
    public interface IServiceCalls
    {
        bool LoginWp(string username, string password);

        void SendEmail(string username, string message);
    }
}
