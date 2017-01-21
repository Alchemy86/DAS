using System;
using DAS.Domain;
using DAS.Domain.Users;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceCalls service;

        private readonly IUserRepository userRepository;

        private readonly ISystemRepository systemRepository;

        private readonly IUnitOfWork unitOfWork;

        public LoginController(IServiceCalls api, IUserRepository userRepository, ISystemRepository systemRepository, IUnitOfWork unitOfWork)
        {
            if (api == null) throw new ArgumentNullException(nameof(api));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (systemRepository == null) throw new ArgumentNullException(nameof(systemRepository));
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            service = api;
            this.userRepository = userRepository;
            this.systemRepository = systemRepository;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml");
            }

            ViewBag.Message = "Login Failed. Please check your details";

            if (!ServiceLogin(model))
            {
                return View("~/Views/Login/Index.cshtml");
            }

            return RedirectToAction("Index", "Bids");
        }

        private bool ServiceLogin(LoginModel model)
        {
            if (!service.LoginWp(model.UserName, model.Password)) return false;
            HttpContext.Session.SetString("SessionToken", Guid.NewGuid().ToString());
            HttpContext.Session.SetString("DASUserName", model.UserName);

            var userAccount = userRepository.GetUserAccount(model.UserName);
            if (userAccount == null)
            {
                userAccount = new User
                {
                    AccountID = Guid.NewGuid(),
                    Username = model.UserName,
                    Password = model.Password,
                    ReceiveEmails = false,
                    AccessLevel = 1,
                    UseAccountForSearch = false
                };
                systemRepository.SaveAccount(userAccount);
                unitOfWork.Save();
            }

            var account = userRepository.GetSessionDetails(model.UserName).GoDaddyAccount;
            HttpContext.Session.SetString("AccountVerified", account == null ? string.Empty : "Verified");

            HttpContext.Session.SetObjectAsJson("UserAccount", userAccount);
            ViewBag.Signout = "Signout";
            return true;
        }
    }
}