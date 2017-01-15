using System;
using System.Collections.Generic;
using DAS.Domain.GoDaddy;

namespace MVC.Models.Pages
{
    public class MyBidsModel
    {
        public IEnumerable<Auction> Auctions { get; private set; }

        public IEnumerable<Auction> HistoricAuctions { get; private set; }

        public MyBidsModel(IEnumerable<Auction> auctions, IEnumerable<Auction> historicAuctions)
        {
            if (auctions == null) throw new ArgumentNullException(nameof(auctions));
            if (historicAuctions == null) throw new ArgumentNullException(nameof(historicAuctions));

            Auctions = auctions;
            HistoricAuctions = historicAuctions;
        }

    }
}
