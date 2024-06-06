using PortfolioManager.Base.Enums;

namespace PortfolioManager.Managers.Managers.Interfaces;

public interface ILogManager
{
    void LogError(string ErroMmessage, ErrorTypes errorType = ErrorTypes.BaseError);
}
