namespace PortfolioManager.Managers.Configurations;

public class MailSettings : IMailSettings
{
    public string Host { get; set; }

    public int Port { get; set; }

    public bool EnableSsl { get; set; }

    public string FromEmail { get; set; }

    public string Password { get; set; }
}
