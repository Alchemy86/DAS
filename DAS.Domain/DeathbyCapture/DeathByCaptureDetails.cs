namespace DAS.Domain.DeathbyCapture
{
    public class DeathByCaptureDetails
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public DeathByCaptureDetails(string userName, string passWord)
        {
            Username = userName;
            Password = passWord;
        }
    }
}
