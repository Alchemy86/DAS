using System;
using DAS.Domain;
using DAS.Domain.Users;
using DAS.ServiceCall.LunchboxcodeAPI;

namespace DAS.ServiceCall
{
    public class DasService : IServiceCalls
    {
        private DomainAuctionSniperAPI Service;
        private ISystemRepository SystemRepository;
        public DasService(ISystemRepository systemRepository)
        {
            SystemRepository = systemRepository;
            Service = new DomainAuctionSniperAPI();
        }
        public bool LoginWP(string username, string password)
        {
            return Service.LoginWP(username, password) == "MATCHED";
        }

        public void SendEmail(string username, string message)
        {
            Service.Email(SystemRepository.AlertEmail, "Bug Report",
                                 "Account: " + username + Environment.NewLine +
                                 "Description: " + message, "Service Manager Bug Report");
        }
    }
}
