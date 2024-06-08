using PortfolioManager.Base.Enums;

namespace PortfolioManager.Managers.Managers.Interfaces;

public interface ILogManager
{
    Task LogErrorAsync(string ErroMmessage, ErrorTypes errorType = ErrorTypes.BaseError);
}
