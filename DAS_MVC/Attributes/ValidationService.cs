using System;
using System.Collections.Generic;
using DAS_MVC.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DAS_MVC.Attributes
{
    public class ValidationService : ActionFilterAttribute
    {

        //private IMobileUnitOfWork _services;
        private Controller _controller;
        private ActionExecutingContext _context;
        private bool ContinueChecks;
        private ISession _session => _context.HttpContext.Session;

    /// <summary>
    /// Ensures the page is validated before being directed
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ContinueChecks = true;
        var controller = context.Controller as BaseController;
        //_services = controller.UnitOfWork;

        _context = context;
        _controller = controller;
        _controller.ViewBag.LoggedIn = true;

        base.OnActionExecuting(context);
        RedirectToLoginIfNoSession();
    }

    /// <summary>
    /// Take the user to the login page if there is no session token
    /// </summary>
    public void RedirectToLoginIfNoSession()
    {
        try
        {
            if (CascadeSessionToken == Guid.Empty)
            {
                RedirectToLogin();
                return;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private Guid CascadeSessionToken
    {
        get
        {
            return _session.GetString("SessionToken") == null ? Guid.Empty : new Guid(_session.GetString("SessionToken"));
        }
        set
        {
            _session.SetString("SessionToken", value.ToString());
        }
    }

    /// <summary>
    /// Redirect to the login page
    /// </summary>
    private void RedirectToLogin(string message = "")
    {
        ContinueChecks = false;
        _context.Result = LoginOverride(message);
    }

    /// <summary>
    /// Redirect to the login page and supply the required warning
    /// </summary>
    /// <returns></returns>
    private IActionResult LoginOverride(string message)
    {
        _controller.ViewBag.LoggedIn = false;
        _controller.ViewBag.Warning = string.IsNullOrEmpty(message) ? "Your session has expired or you do not have access to the page or function you just tried to access." : message;
        return _controller.View("~/Views/Login/Index.cshtml");
    }
}
}
