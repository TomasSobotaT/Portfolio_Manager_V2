using Newtonsoft.Json;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Repositories.Interfaces;
using PortfolioManager.Models.Responses.Currency;

namespace PortfolioManager.ExternalApis.Repositories;
public class CurrencyPriceApiRepository(ICurrencyPriceClient currencyPriceClient) : ICurrencyPriceApiRepository
{
    private readonly ICurrencyPriceClient currencyPriceClient = currencyPriceClient;

    public async Task<decimal?> GetCurrencyPriceAsync(string currencyName)
    {
        var jsonString = await currencyPriceClient.GetCurrencyPriceAsync();
        var result = JsonConvert.DeserializeObject<CurrencyPriceApiResponse>(jsonString);

        if (result is null)
        {
            return null;
        }

        return currencyName.ToLower().Trim() switch
        {
            "usd" => result.kurzy?.USD?.dev_stred,
            "eur" => result.kurzy?.EUR?.dev_stred,
            _ => null,
        };
    }
}
