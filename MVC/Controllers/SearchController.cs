using System;
using System.Collections.Generic;
using System.Linq;
using DAS.Domain;
using DAS.Domain.GoDaddy;
using DAS.Domain.Users;
using DAS.GoDaddyv2;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IAuctionRepository auctionRepository;
        private readonly IUnitOfWork unitOfWork;

        public SearchController(IUserRepository userRepository, IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
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
            var resultsModel = new SearchModel(new List<Auction>(), "", 50);
            return View("~/Views/Search/Index.cshtml", resultsModel);
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Search(SearchModel model)
        {
            var gdService = new GoDaddyAuctionSniper(Username, userRepository);
            var results = gdService.Search(model.SearchText).Take(model.SearchLimit).ToList();
            var accountId = userRepository.GetSessionDetails(Username).GoDaddyAccount.AccountId;
            auctionRepository.RemoveExisting(accountId);
            auctionRepository.SaveAuctionSearch(results, accountId);
            unitOfWork.Save();

            var resultsModel = new SearchModel(results, model.SearchText, model.SearchLimit);
            return View("~/Views/Search/Index.cshtml", resultsModel);
        }

        [HttpPost]
        public string SetAuctionBid(string auctionRef, int amount)
        {
            var gdService = new GoDaddyAuctionSniper(Username, userRepository);
            var auctionFromSearch = auctionRepository.GetAuctionFromSearch(auctionRef);
            if (auctionFromSearch == null)
                throw new NullReferenceException("Auction");

            var endDate = gdService.GetEndDate(auctionFromSearch.AuctionRef);

            var auction = new Auction(Guid.NewGuid(), endDate, auctionFromSearch.EstimateEndDate,
                auctionFromSearch.DomainName,
                auctionFromSearch.AuctionRef, auctionFromSearch.Bids, auctionFromSearch.MinBid, amount,
                auctionFromSearch.Processed,
                auctionFromSearch.Traffic);

            auction.SetAccountLink(userRepository.GetSessionDetails(Username).GoDaddyAccount.AccountId);
            auctionRepository.SaveAuction(auction, gdService.GetPacificTime);

            unitOfWork.Save();

            return auction.DomainName + " saved";
        }
    }
}