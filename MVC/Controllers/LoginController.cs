using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using DAS.ServiceCall.LunchboxcodeAPI;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Http;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceCalls Service;

        public LoginController(IServiceCalls api)
        {
            Service = api;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Login to the site
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml");
            }
            ViewBag.Message = "Login Failed. Please check your details";
            if (ServiceLogin(model))
            {
                HttpContext.Session.SetString("SessionToken", Guid.NewGuid().ToString());
                return View("~/Views/Bids/Index.cshtml");
            }
            return View("~/Views/Login/Index.cshtml");
        }

        /// <summary>
        /// Attempt the service login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool ServiceLogin(LoginModel model)
        {
            if (Service.LoginWP(model.UserName, model.Password))
            {
                return true;
            }
            return false;
        }

    }

}