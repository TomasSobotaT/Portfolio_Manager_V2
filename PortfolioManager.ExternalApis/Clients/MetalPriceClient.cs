using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;

namespace PortfolioManager.ExternalApis.Services;

public class MetalPriceClient(HttpClient httpClient, IExternalApiSettings externalApiSettings) : IMetalPriceClient
{
    private readonly HttpClient httpClient = httpClient;
    private readonly IExternalApiSettings externalApiSettings = externalApiSettings;

    public async Task<string> GetMetalPriceAsync()
    {
        var url = $"{externalApiSettings.MetalPriceApiSuffix1}{externalApiSettings.MetalPriceApiKey}{externalApiSettings.MetalPriceApiSuffix2}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }
}
