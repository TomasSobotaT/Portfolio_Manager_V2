using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Base;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class CommodityRepository(ApplicationDbContext applicationDbContext, IUserContext context) : ExtendedBaseRepository<CommodityEntity, int>(applicationDbContext, context), ICommodityRepository
{
    public async Task<CommodityEntity> GetCommodityByNameAsync(string name)
    {
        return await GetDbSet().FirstOrDefaultAsync(e => e.Name == name);
    }
}
