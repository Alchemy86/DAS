using DAS.Domain.Users;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BugReportController : SessionController
    {
        private readonly IServiceCalls Service;
        public BugReportController(IServiceCalls service, IUserRepository userRepository) : base (userRepository)
        {
            Service = service;
        }

        [HttpPost]
        public string SubmitBug(string message)
        {
            Service.SendEmail(Username, message);
            return "Your report has now been submitted, thank you";
        }
    }
}