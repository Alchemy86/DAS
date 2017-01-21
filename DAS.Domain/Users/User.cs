using System;

namespace DAS.Domain.Users
{
    public class User
    {
        public Guid AccountID;

        public string Username;

        public string Password;

        public int AccessLevel;

        public bool ReceiveEmails;

        public bool UseAccountForSearch;
    }
}
