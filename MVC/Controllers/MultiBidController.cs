using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class MultiBidController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}