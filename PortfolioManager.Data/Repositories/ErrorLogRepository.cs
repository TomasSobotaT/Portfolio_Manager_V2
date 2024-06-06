using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class ErrorLogRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<ErrorLogEntity>(applicationDbContext), IErrorLogRepository
{
}
