using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DAS.Domain;
using DAS.Domain.GoDaddy;
using DAS.Domain.Users;

namespace DAS.DAL2.Repositories
{
    public class SystemRepository : BaseRepository, ISystemRepository
    {
        public SystemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null) { throw new NotImplementedException("IUnitOfWork"); }
        }

        public string ServiceEmail
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "ServiceEmail").Value; }
        }

        public string ServiceEmailPassword
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "ServiceEmailPass").Value; }
        }

        public string ServiceEmailPort
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "ServiceEmailPort").Value; }
        }

        public string ServiceEmailSmtp
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "ServiceEmailSMTP").Value; }
        }

        public string ServiceEmailUser
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "ServiceEmailUser").Value; }
        }

        public string AlertEmail
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "AlertEmail").Value; }
        }

        public IEnumerable<Auction> GetEndingAuctions()
        {
            var tomorrow = Global.GetPacificTime.AddDays(3);
            //var auctions = Context.Auctions.Include("GoDaddyAccount").Where(x => x.Processed == false && x.EndDate <= tomorrow);
            //var moo = Context.Auctions.Where(x => x.Processed == false && x.EndDate <= tomorrow).ToList();
            var results = (from e in Context.Auctions.Include("GoDaddyAccount")
                select e).Where(x => x.Processed == false && x.EndDate <= tomorrow)
                .AsEnumerable()
                .Select(x => x.ToDomainObject());

            return results;
        }

        public void MarkAuctionAsProcess(Guid auctionId)
        {
            var auction = Context.Auctions.First(x => x.AuctionID == auctionId);
            auction.Processed = true;
            Context.Auctions.AddOrUpdate(auction);
            Context.Save();
        }

        public void MarkAlertAsProcesed(Guid alertId)
        {
            var alert = Context.Alerts.First(x => x.AlertID == alertId);
            alert.Processed = true;
            Context.Alerts.AddOrUpdate(alert);
            Context.Save();
        }

        public string BidTime
        {
            get { return Context.SystemConfig.First(x => x.PropertyID == "BidTime").Value; }
        }


        public IEnumerable<Domain.GoDaddy.Alerts.Alert> GetAlerts()
        {
             return (from e in Context.Alerts.Include("Auction")
                           select e).Where(x => x.Processed == false)
                        .AsEnumerable()
                        .Select(x => x.ToDomainObject());
        }

        public void SaveGodaddyAccount(Domain.GoDaddy.Users.GoDaddyAccount account, Guid userAccount)
        {
            var existingRecord = Context.GoDaddyAccount.FirstOrDefault(x => x.UserID == userAccount) ??
                                 new GoDaddyAccount { UserID = userAccount, AccountID = Guid.NewGuid() };

            existingRecord.GoDaddyUsername = account.Username;
            existingRecord.GoDaddyPassword = account.Password;
            existingRecord.Verified = account.Verified;

            Context.GoDaddyAccount.AddOrUpdate(existingRecord);
        }

        public void SaveAccount(User account)
        {
            var existingRecord = Context.Users.FirstOrDefault(x => x.Username == account.Username) ??
                                 new Users { UserID = account.AccountID, Username = account.Username, Password = account.Password };

            existingRecord.Username = account.Username;
            existingRecord.Password = account.Password;
            existingRecord.AccessLevel = account.AccessLevel;
            existingRecord.ReceiveEmails = account.ReceiveEmails;
            existingRecord.UseAccountForSearch = account.UseAccountForSearch;

            Context.Users.AddOrUpdate(existingRecord);
        }
    }
}
