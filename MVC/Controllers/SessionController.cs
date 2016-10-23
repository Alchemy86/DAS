using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAS.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class SessionController : Controller
    {
        private readonly IUserRepository UserRepository;
        public SessionController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        protected string Username
        {
            get
            {
                return HttpContext.Session.GetString("DASUserName") ?? string.Empty;
            }
        }

    }
}
