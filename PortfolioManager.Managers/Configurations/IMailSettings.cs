namespace PortfolioManager.Managers.Configurations;

public interface IMailSettings
{
    string Host { get; }

    int Port { get; }

    bool EnableSsl { get; }

    string FromEmail { get; }

    string Password { get; }
}
