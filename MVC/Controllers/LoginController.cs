using System;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceCalls service;

        public LoginController(IServiceCalls api)
        {
            service = api;
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
            ViewBag.Signout = "Signout";
            return true;
        }
    }
}