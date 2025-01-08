using EncantoApadrinhamento.Core.CustomException;
using EncantoApadrinhamento.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace EncantoApadrinhamento.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            _smtpClient = new SmtpClient(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]!))
            {
                Credentials = new NetworkCredential(_configuration["Email:UserName"], _configuration["Email:Password"]),
                EnableSsl = bool.Parse(_configuration["Email:EnableSsl"]!),
                UseDefaultCredentials = bool.Parse(_configuration["Email:UseDefaultCredentials"]!),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }

        public async Task SendEmailAsync(string email, string subject, string message, bool isBodyHtml = false)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:UserName"]!, _configuration["Email:DisplayName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = isBodyHtml
            };

            mailMessage.To.Add(email);
            mailMessage.Bcc.Add(_configuration["Email:UserName"]!);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DomainException($"Erro ao enviar email: {ex.Message}");
            }
        }

        public async Task SendEmailAsync(List<string> emails, string subject, string message, bool isBodyHtml)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:UserName"]!, _configuration["Email:DisplayName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = isBodyHtml
            };

            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
