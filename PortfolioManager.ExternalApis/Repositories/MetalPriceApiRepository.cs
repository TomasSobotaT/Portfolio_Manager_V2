using Newtonsoft.Json;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Repositories.Interfaces;
using PortfolioManager.Models.Responses.Metal;

namespace PortfolioManager.ExternalApis.Repositories;
public class MetalPriceApiRepository(IMetalPriceClient metalPriceClient) : IMetalPriceApiRepository
{
    private readonly IMetalPriceClient metalPriceClient = metalPriceClient;

    public async Task<decimal?> GetMetalPriceAsync(string metalName)
    {
        var jsonString = await metalPriceClient.GetMetalPriceAsync();
        var result = JsonConvert.DeserializeObject<MetalPriceApiResponse>(jsonString);

        if (result is null)
        {
            return null;
        }

        return metalName.ToLower().Trim() switch
        {
            "gold" => result?.GSJ.Gold?.USD?.Ask,
            "silver" => result?.GSJ?.Silver?.USD?.Ask,
            _ => null,
        };
    }
}
