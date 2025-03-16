using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.ExternalApis.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;

namespace PortfolioManager.Managers.Services;
public class UpdateDatabaseService(
    ICryptoPriceApiRepository cryptoPriceApiRepository,
    ICurrencyPriceApiRepository currencyPriceApiRepository,
    IMetalPriceApiRepository metalPriceApiRepository,
    ICommodityRepository commodityRepository,
    ICurrencyRepository currencyRepository)
    : IUpdateDatabaseService
{
    public async Task UpdateDatabaseAsync()
    {
        var comoditiesEntities = await commodityRepository.GetAllAsync();

        foreach (var comodityEntity in comoditiesEntities)
        {
            if(comodityEntity.Name.Equals("bitcoin", StringComparison.CurrentCultureIgnoreCase))
            {
                var price = await cryptoPriceApiRepository.GetCryptoPriceAsync();

                if(price is not null)
                {
                    comodityEntity.ExchangeRate = price.Value;
                    commodityRepository.Update(comodityEntity);
                    Console.WriteLine($"Proběhl update bitcoinu na {comodityEntity.ExchangeRate}");
                }
            }

            if (comodityEntity.Name.Equals("zlato", StringComparison.CurrentCultureIgnoreCase))
            {
                var price = await metalPriceApiRepository.GetMetalPriceAsync("gold");

                if (price is not null)
                {
                    comodityEntity.ExchangeRate = price.Value;
                    commodityRepository.Update(comodityEntity);
                    Console.WriteLine($"Proběhl update zlata na {comodityEntity.ExchangeRate}");
                }
            }
        }

        var currencyEntities = await currencyRepository.GetAllAsync();

        foreach (var currencyEntity in currencyEntities)
        {
            if (currencyEntity.Name.Equals("czk", StringComparison.CurrentCultureIgnoreCase))
            {
                var price = await currencyPriceApiRepository.GetCurrencyPriceAsync("usd");

                if (price is not null)
                {
                    currencyEntity.ExchangeRate = price.Value;
                    currencyRepository.Update(currencyEntity);
                    Console.WriteLine($"Proběhl update meny czk na {currencyEntity.ExchangeRate}");
                }
            }
        }
    }
}
