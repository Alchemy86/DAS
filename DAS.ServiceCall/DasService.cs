using System;
using DAS.Domain;
using DAS.ServiceCall.LunchboxcodeAPI;

namespace DAS.ServiceCall
{
    public class DasService : IServiceCalls
    {
        private readonly DomainAuctionSniperAPI service;

        private readonly ISystemRepository systemRepository;

        public DasService(ISystemRepository systemRepository)
        {
            this.systemRepository = systemRepository;
            service = new DomainAuctionSniperAPI();
        }

        public bool LoginWp(string username, string password)
        {
            return service.LoginWP(username, password) == "MATCHED";
        }

        public void SendEmail(string username, string message)
        {
            service.Email(systemRepository.AlertEmail, "Bug Report",
                                 "Account: " + username + Environment.NewLine +
                                 "Description: " + message, "Service Manager Bug Report");
        }
    }
}
