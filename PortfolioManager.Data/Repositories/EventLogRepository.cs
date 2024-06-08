using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class EventLogRepository(ApplicationDbContext applicationDbContext, IUserContext userContext)
    : BaseRepository<EventLogEntity>(applicationDbContext, userContext), IEventLogRepository
{
}
