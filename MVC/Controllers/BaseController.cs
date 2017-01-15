using System;
using Microsoft.AspNetCore.Mvc;
using MVC.Attributes;

namespace MVC.Controllers
{
    [ValidationService]
    public class BaseController : SessionController
    {
        public IActionResult FAQ()
        {
            return View("~/Views/User/FAQ.cshtml");
        }

        public IActionResult Admin()
        {
            return View("~/Views/Admin/Index.cshtml");
        }
        public IActionResult Login()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.Message = "You have been logged out";
            ViewBag.Signout = "";
            return View("~/Views/Login/Index.cshtml");
        }

        protected DateTime GetPacificTime
        {
            get
            {
                var tzi = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

                return localDateTime;
            }
        }
    }
}