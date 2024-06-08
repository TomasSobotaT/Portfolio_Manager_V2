using PortfolioManager.Base.Enums;

namespace PortfolioManager.Managers.Managers.Interfaces;

public interface ILogManager
{
    Task LogErrorAsync(string ErroMmessage, string userIpAdress, ErrorTypes errorType = ErrorTypes.BaseError);
    Task LogEventAsync(string userIpAdress, EventTypes evetType = EventTypes.BaseEvent);
}
