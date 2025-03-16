using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Repositories.Base;

namespace PortfolioManager.Data.Repositories.Interfaces;

public interface ICommodityRepository : IExtendedBaseRepository<CommodityEntity, int>
{
    Task<CommodityEntity> GetCommodityByNameAsync(string name);
}
