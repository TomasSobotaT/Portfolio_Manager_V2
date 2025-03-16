using Newtonsoft.Json;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Repositories.Interfaces;
using PortfolioManager.Models.Responses.Crypto;

namespace PortfolioManager.ExternalApis.Repositories;
public class CryptoPriceApiRepository(ICryptoPriceClient cryptoPriceClient) : ICryptoPriceApiRepository
{
    private readonly ICryptoPriceClient cryptoPriceClient = cryptoPriceClient;

    public async Task<decimal?> GetCryptoPriceAsync(string cryptoName = "bitcoin", string currencyName = "usd")
    {
        var jsonString = await cryptoPriceClient.GetCryptoPriceAsync(cryptoName, currencyName);
        var result = JsonConvert.DeserializeObject<Dictionary<string, CryptoPriceApiResponse>>(jsonString);

        if (result is null || result.Count == 0)
        {
            return null;
        }

        if (currencyName.Equals("czk", StringComparison.OrdinalIgnoreCase))
            return result.FirstOrDefault().Value?.czk;

        return result.FirstOrDefault().Value?.usd;
    }
}
