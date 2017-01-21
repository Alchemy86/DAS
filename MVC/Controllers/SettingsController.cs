using System;
using DAS.Domain;
using DAS.Domain.GoDaddy.Users;
using DAS.Domain.Users;
using DAS.GoDaddyv2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IUserRepository userRepository;

        private readonly ISystemRepository systemRepository;

        private readonly IUnitOfWork unitOfWork;

        public SettingsController(IUserRepository userRepository, ISystemRepository systemRepository, IUnitOfWork unitOfWork)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (systemRepository == null) throw new ArgumentNullException(nameof(systemRepository));
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.userRepository = userRepository;
            this.systemRepository = systemRepository;
            this.unitOfWork = unitOfWork;
            ViewBag.Settings = "true";
        }

        public IActionResult Index()
        {
            var model = new SettingsModel();
            var data = userRepository.GetSessionDetails(Username).GoDaddyAccount;
            var user = userRepository.GetUserAccount(Username);
            if (data != null)
            {
                model.Username = data.Username;
                model.Password = data.Password;
                model.Verified = data.Verified;
            }

            model.EmailAlerts = user.ReceiveEmails;
            model.UseAccountInSearch = user.UseAccountForSearch;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult VerifyAccount(SettingsModel model)
        {
            ViewBag.ResponseFail = "Login Failed. Please try again";
            var gd = new GoDaddyAuctionSniper(Username, userRepository);
            if (gd.Login(0, model.Username, model.Password))
            {
                ViewBag.Response = "Account Verified";
                ViewBag.ResponseFail = string.Empty;
                systemRepository.SaveGodaddyAccount(new GoDaddyAccount(Guid.NewGuid(), model.Username, model.Password, true), UserAccount.AccountID);
                systemRepository.SaveAccount(new User
                {
                    AccountID = UserAccount.AccountID,
                    Password = UserAccount.Password,
                    Username = UserAccount.Username,
                    ReceiveEmails = model.EmailAlerts,
                    UseAccountForSearch = model.UseAccountInSearch
                } );
                unitOfWork.Save();
                HttpContext.Session.SetString("AccountVerified", "Verified");
            }

            return View("~/Views/Settings/Index.cshtml");
        }

        [HttpPost]
        public void UpdateEmailSetting(bool receiveEmails)
        {
            var account = userRepository.GetUserAccount(Username);
            account.ReceiveEmails = receiveEmails;
            systemRepository.SaveAccount(account);
            unitOfWork.Save();
        }

        [HttpPost]
        public void UpdateSearchSetting(bool useAccount)
        {
            var account = userRepository.GetUserAccount(Username);
            account.UseAccountForSearch = useAccount;
            systemRepository.SaveAccount(account);
            unitOfWork.Save();
        }
    }
}