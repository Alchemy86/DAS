using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BidsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}