using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Repositories.Base;

namespace PortfolioManager.Data.Repositories.Interfaces;

public interface ICurrencyRepository : IExtendedBaseRepository<CurrencyEntity, int>
{
    Task<CurrencyEntity> GetCurrencyByNameAsync(string name);
}
