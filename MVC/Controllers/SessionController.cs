using System;
using DAS.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class SessionController : Controller
    {
        protected string Username => HttpContext.Session.GetString("DASUserName") ?? string.Empty;

        protected User UserAccount => HttpContext.Session.GetString("SessionToken") == null ? new User() : HttpContext.Session.GetObjectFromJson<User>("UserAccount");

        public Guid SessionToken
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

        public string AccountVerified
        {
            get
            {
               return HttpContext.Session.GetString("AccountVerified") ?? string.Empty;
            }
        }
    }
}
