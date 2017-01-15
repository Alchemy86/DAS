using System;
using System.Collections.Generic;
using System.Linq;
using DAS.Domain;
using DAS.Domain.Auctions;
using DAS.Domain.GoDaddy;
using DAS.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class BidsController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IAuctionRepository auctionRepository;
        private readonly IUnitOfWork unitOfWork;

        public BidsController(IUserRepository userRepository, IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (auctionRepository == null) throw new ArgumentNullException(nameof(auctionRepository));
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.userRepository = userRepository;
            this.auctionRepository = auctionRepository;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var accountId = userRepository.GetSessionDetails(Username).GoDaddyAccount.AccountId;
            var records = auctionRepository.GetUsersAuctions(accountId).ToList();
            var historicRecords = records.Where(x => x.EndDate < GetPacificTime).OrderByDescending(x => x.EndDate);
            var currentRecords = records.Where(x => x.EndDate >= GetPacificTime).OrderBy(x => x.EndDate); ;
            var model = new MyBidsModel(currentRecords, historicRecords);

            return View(model);
        }

        [HttpPost]
        public IActionResult GetAuctionHistory(string auctionId)
        {
            var guid = Guid.Parse(auctionId);
            return PartialView("~/Views/Shared/_AuctionHistoryPartial.cshtml", auctionRepository.GetAuctionHistory(guid));
        }
    }
}