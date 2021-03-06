﻿using System;
using System.Collections.Generic;

namespace DAS.Domain.GoDaddy
{
    public interface IAuctionRepository
    {
        Auction GetAuctionFromSearch(string auctionRef);

        void SaveAuction(Auction auction, DateTime pacificTime);

        void SaveAuctionSearch(IEnumerable<Auction> auctions, Guid accountId);

        IEnumerable<Auction> GetUsersAuctions(Guid accountId);

        void UpdateAuctionBid(Guid auctionGuid, int bid, DateTime pacificTime);

        IEnumerable<Auctions.AuctionHistory> GetAuctionHistory(Guid auctionGuid);

        void RemoveExisting(Guid accountId);

        void DeleteAuction(Guid auctionId, Guid accountId);

        void AddHistoryRecord(Guid auctionGuid, string message, DateTime pacificTime);
    }
}
