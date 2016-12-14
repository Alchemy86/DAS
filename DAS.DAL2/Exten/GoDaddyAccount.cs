namespace DAS.DAL2
{
    public partial class GoDaddyAccount
    {
        public Domain.GoDaddy.Users.GoDaddyAccount ToDomainObject()
        {
            return new Domain.GoDaddy.Users.GoDaddyAccount(AccountID, GoDaddyUsername, GoDaddyPassword, Verified);
        }
    }
}
