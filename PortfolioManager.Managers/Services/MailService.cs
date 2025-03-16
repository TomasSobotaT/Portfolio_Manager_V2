using PortfolioManager.Managers.Configurations;
using PortfolioManager.Managers.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace PortfolioManager.Managers.Services
{
    public class MailService(IMailSettings mailSettings) : IMailService
    {
        public async Task SendEmailAsync(string fromEmail, string subject, string body, string displayName, params string[] recipients)
        {
            var fromAddress = new MailAddress(fromEmail, displayName);

            using var mailMessage = new MailMessage()
            {
                Subject = subject,
                From = fromAddress,
                Body = body,
                IsBodyHtml = true,
            };

            using var smtpClient = new SmtpClient()
            {
                Host = mailSettings.Host,
                Port = mailSettings.Port,
                EnableSsl = mailSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailSettings.FromEmail, mailSettings.Password),
            };

            foreach (var recipient in recipients)
            {
                mailMessage.To.Add(recipient);
            }

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}
