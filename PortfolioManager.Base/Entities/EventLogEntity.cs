using PortfolioManager.Base.Enums;

namespace PortfolioManager.Base.Entities;

public class EventLogEntity : BaseEntity
{
    public int? UserId { get; set; }

    public string UserIpAdress { get; set; }

    public EventTypes EventType { get; set; }

}
