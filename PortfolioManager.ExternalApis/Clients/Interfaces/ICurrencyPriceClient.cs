

namespace PortfolioManager.ExternalApis.Clients.Interfaces;

public interface ICurrencyPriceClient
{
    Task<string> GetCurrencyPriceAsync();
}
