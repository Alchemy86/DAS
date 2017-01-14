using System;
using System.Collections.Generic;
using System.Linq;
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

        public SearchController(IUserRepository userRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var model = new SearchModel(new List<Auction>(), "", 50);
            return View(model);
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Search(SearchModel model)
        {
            var gdService = new GoDaddyAuctionSniper(Username, userRepository);
            var results = gdService.Search(model.SearchText).Take(model.SearchLimit);
            var resultsModel = new SearchModel(results, model.SearchText, model.SearchLimit);
            return View("~/Views/Search/Index.cshtml", resultsModel);
        }
    }
}