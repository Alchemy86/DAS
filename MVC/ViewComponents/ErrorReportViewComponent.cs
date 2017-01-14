using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.ViewComponents
{
    [ViewComponent(Name = "ErrorReport")]
    public class ErrorReportViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/_ErrorReportPartial.cshtml", new ErrorReport());
        }
    }
}
