using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;
using System.Net.Http.Headers;

namespace PortfolioManager.Data.Repositories;

public class CommodityRepository(ApplicationDbContext applicationDbContext, IUserContext userContext)
    : BaseRepository<CommodityEntity>(applicationDbContext, userContext), ICommodityRepository
{
    public async Task< List<CommodityEntity> >getalll()
    {
        return await this.dbSet.ToListAsync();
    }
}
