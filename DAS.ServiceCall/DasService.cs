using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAS.ServiceCall.LunchboxcodeAPI;

namespace DAS.ServiceCall
{
    public class DasService : IServiceCalls
    {
        private DomainAuctionSniperAPI Service;
        public DasService()
        {
            Service = new DomainAuctionSniperAPI();
        }
        public bool LoginWP(string username, string password)
        {
            return Service.LoginWP(username, password) == "MATCHED";
        }
    }
}
