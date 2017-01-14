using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class SessionController : Controller
    {
        protected string Username => HttpContext.Session.GetString("DASUserName") ?? string.Empty;

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
    }
}
