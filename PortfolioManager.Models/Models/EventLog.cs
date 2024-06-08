using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;

namespace PortfolioManager.Models.Models;

public class EventLog(EventTypes eventType)
{
    public int? UserId { get; set; }

    public string UserIpAdress { get; set; }

    public EventTypes EventType { get; set; } = eventType;

}

