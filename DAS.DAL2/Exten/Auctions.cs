using DAS.Domain.GoDaddy;

namespace DAS.DAL2
{
    public partial class Auctions
    {
        public Auction ToDomainObject()
        {
            return new Auction(AuctionID, EndDate, EstimateEndDate, DomainName, AuctionRef, BidCount, MinBid, MyBid, Processed, Traffic);
        }
    }
}
