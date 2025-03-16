
namespace PortfolioManager.Managers.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(string fromEmail, string subject, string body, string displayName, params string[] recipients);
}
