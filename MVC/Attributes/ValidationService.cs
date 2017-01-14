using System;
using MVC.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Attributes
{
    public class ValidationService : ActionFilterAttribute
    {

        //private IMobileUnitOfWork _services;
        private Controller controller;
        private ActionExecutingContext context;
        private ISession Session => context.HttpContext.Session;

    /// <summary>
    /// Ensures the page is validated before being directed
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        controller = context.Controller as BaseController;

        this.context = context;
        var o = controller;
        if (o != null) o.ViewBag.LoggedIn = true;

        base.OnActionExecuting(context);
        RedirectToLoginIfNoSession();
    }

    /// <summary>
    /// Take the user to the login page if there is no session token
    /// </summary>
    public void RedirectToLoginIfNoSession()
    {
        if (CascadeSessionToken == Guid.Empty)
        {
            controller.ViewBag.Signout = "";
            RedirectToLogin();
        }
        else
        {
            controller.ViewBag.Signout = "Signout";
        }
    }

    private Guid CascadeSessionToken => Session.GetString("SessionToken") == null ? Guid.Empty : new Guid(Session.GetString("SessionToken"));

        /// <summary>
    /// Redirect to the login page
    /// </summary>
    private void RedirectToLogin(string message = "")
    {
        context.Result = LoginOverride(message);
    }

    /// <summary>
    /// Redirect to the login page and supply the required warning
    /// </summary>
    /// <returns></returns>
    private IActionResult LoginOverride(string message)
    {
        controller.ViewBag.LoggedIn = false;
        controller.ViewBag.Warning = string.IsNullOrEmpty(message) ? "Your session has expired or you do not have access to the page or function you just tried to access." : message;
        return controller.View("~/Views/Login/Index.cshtml");
    }
}
}
