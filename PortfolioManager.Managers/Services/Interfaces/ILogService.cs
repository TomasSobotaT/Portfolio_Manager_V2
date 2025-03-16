
namespace PortfolioManager.Managers.Services.Interfaces;

public interface ILogService
{
    void LogCustomError(string message, Exception exception);

    void LogCustomInformation(string message);
}
