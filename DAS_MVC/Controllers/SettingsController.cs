using DAS_MVC.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace DAS_MVC.Controllers
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