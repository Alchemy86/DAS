using System;
using DAS_MVC.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAS_MVC.Controllers
{
    [ValidationService]
    public class BaseController : Controller
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

        public Guid CascadeSessionToken
        {
            get
            {
                return HttpContext.Session.GetString("SessionToken") == null ? Guid.Empty : new Guid(HttpContext.Session.GetString("SessionToken"));
            }
            set
            {
                HttpContext.Session.SetString("SessionToken", value.ToString());
            }
        }
    }
}