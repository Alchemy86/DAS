using System;

namespace DAS.Domain.GoDaddy.Users
{
    public class GoDaddyAccount
    {
        public Guid AccountId { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public bool Verified { get; private set; }

        public GoDaddyAccount(Guid accountId, string userName, string password, bool verified)
        {
            AccountId = accountId;
            Username = userName;
            Password = password;
            Verified = verified;
        }
    }
}
