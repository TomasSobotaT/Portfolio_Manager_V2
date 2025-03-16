
namespace PortfolioManager.ExternalApis.Clients.Interfaces;

public interface ICryptoPriceClient
{
    Task<string> GetCryptoPriceAsync(string cryptoName, string currencyName);
}
