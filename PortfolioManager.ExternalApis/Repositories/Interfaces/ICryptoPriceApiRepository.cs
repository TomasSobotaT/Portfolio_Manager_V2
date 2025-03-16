

namespace PortfolioManager.ExternalApis.Repositories.Interfaces;

public interface ICryptoPriceApiRepository
{
    Task<decimal?> GetCryptoPriceAsync(string cryptoName = "bitcoin", string currencyName = "usd");
}
