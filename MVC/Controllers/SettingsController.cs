using Microsoft.AspNetCore.Mvc;
using MVC.Models.Pages;

namespace MVC.Controllers
{
    public class SettingsController : BaseController
    {
        public SettingsController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyAccount(SettingsModel model)
        {
            return View();
        }
    }
}