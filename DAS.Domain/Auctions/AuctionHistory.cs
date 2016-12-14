using System;
namespace DAS.Domain.Auctions
{
    public class AuctionHistory
    {
        public string Text {  get; private set; }

        public DateTime EventDate { get; private set; }

        public AuctionHistory(DateTime eventDate, string text)
        {
            Text = text;
            EventDate = eventDate;
        }
    }
}
