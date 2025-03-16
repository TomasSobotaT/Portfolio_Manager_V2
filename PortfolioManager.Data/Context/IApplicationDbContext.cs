using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Context;

public interface IApplicationDbContext
{
    DbSet<CommodityEntity> Commodities { get; set; }

    DbSet<CurrencyEntity> Currencies { get; set; }

    DbSet<RecordEntity> Records { get; set; }

    DbSet<UserDocumentEntity> UserDocuments { get; set; }

}