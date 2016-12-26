using System;
using System.Net;
using System.Net.Mail;

namespace DAS.Domain
{
    public class Email : IEmail
    {
        private readonly ISystemRepository systemProperSystemRepository;
        public Email(ISystemRepository repository)
        {
            systemProperSystemRepository = repository;
        }

        private SmtpClient SmtpClient()
        {
            var smtp = systemProperSystemRepository.ServiceEmailSmtp;
            var port = systemProperSystemRepository.ServiceEmailPort;
            var user = systemProperSystemRepository.ServiceEmailUser;
            var password = systemProperSystemRepository.ServiceEmailPassword;

            var client = new SmtpClient(smtp, int.Parse(port))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(user, password)
            };

            return client;
        }

        public void SendEmail(string to, string subject, string message, DateTime timeStamp)
        {
            var email = systemProperSystemRepository.ServiceEmail;
            var mail = new MailMessage(email, to)
            {
                Subject = subject,
                Body = message + Environment.NewLine + timeStamp.ToString("dd/M/yyyy H:mm:ss")
            };

            SmtpClient().Send(mail);
        }
    }
}
