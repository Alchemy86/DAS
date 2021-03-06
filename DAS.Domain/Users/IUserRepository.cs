﻿using System;
using DAS.Domain.GoDaddy.Users;

namespace DAS.Domain.Users
{
    public interface IUserRepository
    {
        GoDaddySession GetSessionDetails(string username);

        void LogError(string message, string type = "Error");

        void AddHistoryRecord(string message, Guid auctionLink);

        User GetUserAccount(string userName);
    }
}