using System;
using System.Collections.Generic;
using System.ComponentModel;
using DAS.Domain.GoDaddy;

namespace MVC.Models.Pages
{
    public class MyBidsModel
    {
        public IEnumerable<Auction> Auctions { get; private set; }

        public IEnumerable<Auction> HistoricAuctions { get; private set; }

        public PageMode PageMode { get; private set; }

        public MyBidsModel(IEnumerable<Auction> auctions, IEnumerable<Auction> historicAuctions, PageMode pageMode)
        {
            if (auctions == null) throw new ArgumentNullException(nameof(auctions));
            if (historicAuctions == null) throw new ArgumentNullException(nameof(historicAuctions));
            if (!Enum.IsDefined(typeof(PageMode), pageMode))
                throw new InvalidEnumArgumentException(nameof(pageMode), (int) pageMode, typeof(PageMode));

            Auctions = auctions;
            HistoricAuctions = historicAuctions;
            PageMode = pageMode;
        }

    }

    public enum PageMode
    {
        View,
        Edit
    }
}
