using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DAS.Domain;
using DAS.Domain.Enum;
using DAS.Domain.GoDaddy;
using DAS.Domain.GoDaddy.Alerts;

namespace DAS.DAL2.Repositories
{
    public class AuctionRepository : BaseRepository, IAuctionRepository
    {
        public AuctionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Auction GetAuctionFromSearch(string auctionRef)
        {
            var auction = Context.AuctionSearch.FirstOrDefault(x => x.AuctionRef == auctionRef);
            if (auction == null)
                return null;

            return new Auction(Guid.NewGuid(), auction.EndDate, auction.EstimateEndDate, auction.DomainName,
                auction.AuctionRef, auction.BidCount, auction.MinBid, auction.MyBid, auction.Processed,
                auction.Traffic);
        }

        public IEnumerable<Domain.Auctions.AuctionHistory> GetAuctionHistory(Guid auctionGuid)
        {
            var records = Context.AuctionHistory.Where(x => x.AuctionLink == auctionGuid).OrderBy(x => x.CreatedDate).ToList();

            return records.Select(record => new Domain.Auctions.AuctionHistory(record.CreatedDate, record.Text)).ToList();
        }

        public IEnumerable<Auction> GetUsersAuctions(Guid accountId)
        {
            var auctions = new List<Auction>();
            var oldestAllowed = DateTime.Now.AddYears(-1);
            var a = Context.Auctions.Where(x => x.AccountID == accountId && x.EndDate > oldestAllowed).ToList();

            foreach (var auction in a)
            {
                auctions.Add(new Auction(auction.AuctionID, auction.EndDate, auction.EstimateEndDate, auction.DomainName,
                            auction.AuctionRef, auction.BidCount, auction.MinBid, auction.MyBid, auction.Processed,
                            auction.Traffic));
            }

            return auctions;
        }

        public void SaveAuctionSearch(IEnumerable<Auction> auctions, Guid accountId)
        {
            var enumerable = auctions as IList<Auction> ?? auctions.ToList();
            var auctionData = from a in enumerable
                            select new AuctionSearch
                            {
                                AuctionRef = a.AuctionRef,
                                AccountID = accountId,
                                AuctionID = a.AuctionId,
                                DomainName = a.DomainName,
                                BidCount = a.Bids,
                                BuyItNow = 0,
                                EndDate = a.EndDate,
                                EstimateEndDate = a.EstimateEndDate,
                                MinBid = a.MinBid,
                                MinOffer = 0,
                                MyBid = 0,
                                Price = 0,
                                Traffic = a.Traffic
                            };

            Context.AuctionSearch.AddRange(auctionData);
        }

        public void RemoveExisting(Guid accountId)
        {
            Context.AuctionSearch.RemoveRange(Context.AuctionSearch.Where(x => x.AccountID == accountId));
        }

        public void SaveAuction(Auction auction, DateTime pacificTime)
        {
            var existingAuction = Context.Auctions.Include("Alerts").FirstOrDefault(x => x.AuctionRef == auction.AuctionRef && x.AccountID == auction.AccountId) ??
                                  new Auctions();

            existingAuction.AuctionRef = auction.AuctionRef;
            existingAuction.AccountID = auction.AccountId;
            existingAuction.BidCount = auction.Bids;
            existingAuction.DomainName = auction.DomainName;
            existingAuction.EndDate = auction.EndDate;
            existingAuction.EstimateEndDate = auction.EstimateEndDate;
            existingAuction.MinBid = auction.MinBid;
            existingAuction.MyBid = auction.MyBid;
            existingAuction.Processed = auction.Processed;
            existingAuction.Traffic = auction.Traffic;
            existingAuction.AuctionID = auction.AuctionId;

            var item = new AuctionHistory
            {
                HistoryID = Guid.NewGuid(),
                Text = "Auction Added",
                CreatedDate = pacificTime,
                AuctionLink = existingAuction.AuctionID
            };
            Context.AuctionHistory.Add(item);

            if (existingAuction.Alerts.Count == 0)
            {
                var winalert = new Alerts
                {
                    AlertID = Guid.NewGuid(),
                    AuctionID = existingAuction.AuctionID,
                    Custom = false,
                    Description = AlertType.Win.ToName(),
                    TriggerTime = auction.EndDate.AddMinutes(5),
                    AlertType = AlertType.Win.ToName()
                };
                existingAuction.Alerts.Add(winalert);

                var bidalert = new Alerts
                {
                    AlertID = Guid.NewGuid(),
                    AuctionID = existingAuction.AuctionID,
                    Custom = false,
                    Description = AlertType.Reminder12Hours.ToName(),
                    TriggerTime = auction.EndDate.AddHours(-12),
                    AlertType = AlertType.Reminder12Hours.ToName()
                };
                existingAuction.Alerts.Add(bidalert);

                var bidalert2 = new Alerts
                {
                    AlertID = Guid.NewGuid(),
                    AuctionID = existingAuction.AuctionID,
                    Custom = false,
                    Description = AlertType.Reminder12Hours.ToName(),
                    TriggerTime = auction.EndDate.AddHours(-1),
                    AlertType = AlertType.Reminder1Hour.ToName()
                };
                existingAuction.Alerts.Add(bidalert2);
            }

            Context.Auctions.AddOrUpdate(existingAuction);
        }
    }
}
