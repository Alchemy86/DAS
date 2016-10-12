using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAS_MVC.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using DAS.ServiceCall.LunchboxcodeAPI;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Http;

namespace DAS_MVC.Controllers
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

        public IActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/Login/Index.cshtml");
            }
            ViewBag.Message = "Failure";
            if (ServiceLogin(model))
            {
                HttpContext.Session.SetString("SessionToken", Guid.NewGuid().ToString());
                ViewBag.Message = "Success";
            }
            return View("~/Views/Login/Index.cshtml");
        }

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