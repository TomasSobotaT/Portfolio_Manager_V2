using PortfolioManager.Managers.Services.Interfaces;

namespace PortfolioManager.Managers.Services;
public class LogService(Serilog.ILogger logger) : ILogService
{
    private readonly Serilog.ILogger logger = logger.ForContext("CustomLog", true);

    public void LogCustomInformation(string message)
    {
        logger.Information(message);
    }

    public void LogCustomError(string message, Exception exception)
    {
        logger.Error(exception, message);
    }
}
