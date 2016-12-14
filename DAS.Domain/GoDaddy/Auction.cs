using System;

namespace DAS.Domain.GoDaddy
{
    public class Auction
    {
        public Guid AuctionId { get; private set; }

        public string DomainName { get; private set; }

        public string AuctionRef { get; private set; }

        public int MinBid { get; private set; }

        public DateTime EndDate { get; private set; }

        public int? MyBid { get; private set; }

        public bool Processed { get; private set; }

        public int Bids { get; private set; }

        public Auction(Guid id, DateTime endDate, string domainName, string auctionRef, int bids, int minBid, int? myBid, bool processed)
        {
            AuctionId = id;
            EndDate = endDate;
            DomainName = domainName;
            AuctionRef = auctionRef;
            Bids = bids;
            MinBid = minBid;
            MyBid = myBid;
            Processed = processed;
        }
    }
}
