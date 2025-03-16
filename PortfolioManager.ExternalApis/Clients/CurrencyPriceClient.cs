using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;

namespace PortfolioManager.ExternalApis.Services;

public class CurrencyPriceClient(HttpClient httpClient, IExternalApiSettings externalApiSettings) : ICurrencyPriceClient
{
    private readonly HttpClient httpClient = httpClient;
    private readonly IExternalApiSettings externalApiSettings = externalApiSettings;

    public async Task<string> GetCurrencyPriceAsync()
    {
        var url = $"{externalApiSettings.CurrencyPriceApiSuffix1}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }
}
