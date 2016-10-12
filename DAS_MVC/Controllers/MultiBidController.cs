using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DAS_MVC.Controllers
{
    public class MultiBidController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}