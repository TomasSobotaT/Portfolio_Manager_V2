
namespace PortfolioManager.ExternalApis.Repositories.Interfaces;

public interface ICurrencyPriceApiRepository
{
    Task<decimal?> GetCurrencyPriceAsync(string currencyName);
}
