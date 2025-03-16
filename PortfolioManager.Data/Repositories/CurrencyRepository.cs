using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Base;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class CurrencyRepository(ApplicationDbContext applicationDbContext, IUserContext context) : ExtendedBaseRepository<CurrencyEntity, int>(applicationDbContext, context), ICurrencyRepository
{
    public async Task<CurrencyEntity> GetCurrencyByNameAsync(string name)
    {
        return await GetDbSet().FirstOrDefaultAsync(e => e.Name == name);
    }
}
