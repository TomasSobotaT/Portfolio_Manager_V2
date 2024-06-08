using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Context;

public interface IApplicationDbContext
{
    DbSet<CommodityEntity> Commodities { get; set; }

    DbSet<ErrorLogEntity> ErrorLogs { get; set; }

    DbSet<PriceEntity> Prices { get; set; }

    DbSet<RecordEntity> Records { get; set; }
    DbSet<EventLogEntity> EventLogs { get; set; }
}