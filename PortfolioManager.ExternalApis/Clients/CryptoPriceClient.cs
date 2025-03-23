using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;

namespace PortfolioManager.ExternalApis.Services;

public class CryptoPriceClient(HttpClient httpClient, IExternalApiSettings externalApiSettings) : ICryptoPriceClient
{
    public async Task<string> GetCryptoPriceAsync(string cryptoName, string currencyName)
    {
        var url = $"{externalApiSettings.CryptoPriceApiSuffix1}{cryptoName.ToLower().Trim()}{externalApiSettings.CryptoPriceApiSuffix2}{currencyName.ToLower().Trim()}";

        var response = await httpClient.GetAsync(url);

        if(!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }
}
