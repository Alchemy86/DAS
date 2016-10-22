using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class BugReportController : Controller
    {
        [HttpPost]
        public string SubmitBug(string message)
        {
            ViewBag.BugReportMessage = "Stuff Happened Yo";
            return message;
        }
    }
}