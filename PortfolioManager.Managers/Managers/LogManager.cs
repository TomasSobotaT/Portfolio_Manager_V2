using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Managers.Managers.Interfaces;
namespace PortfolioManager.Managers.Managers;

public class LogManager : ILogManager
{

    public void LogError(string ErroMmessage, ErrorTypes errorType = ErrorTypes.BaseError)
    {
        var errorLogEntity = new ErrorLogEntity(ErroMmessage, errorType);

    }
}
