using System;
using DAS.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BugReportController : SessionController
    {
        private readonly IEmail emailService;
        private readonly ISystemRepository systemRepository;

        public BugReportController(IEmail emailService, ISystemRepository systemRepository)
        {
            if (emailService == null) throw new ArgumentNullException(nameof(emailService));
            if (systemRepository == null) throw new ArgumentNullException(nameof(systemRepository));

            this.emailService = emailService;
            this.systemRepository = systemRepository;
        }

        [HttpPost]
        public string SubmitBug(string message)
        {
            emailService.SendEmail(
                systemRepository.AlertEmail, 
                "Bug Report", 
                message + Environment.NewLine + Environment.NewLine + "Reported By: " + Username, DateTime.Now);
            return "Bug Succesfully Submitted";
        }
    }
}