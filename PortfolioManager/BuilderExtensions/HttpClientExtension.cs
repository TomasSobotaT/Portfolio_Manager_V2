using PortfolioManager.ExternalApis.Clients;
using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;
using PortfolioManager.ExternalApis.Services;

namespace PortfolioManager.BuilderExtensions;

public static class HttpClientExtension
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var externalApiUrls = configuration.GetSection("ExternalApiSettings").Get<ExternalApiSettings>();

        AddCustomClient<ICryptoPriceClient, CryptoPriceClient>(services, externalApiUrls.CryptoPriceApiBaseUrl);
        AddCustomClient<IMetalPriceClient, MetalPriceClient>(services, externalApiUrls.MetalPriceApiBaseUrl);
        AddCustomClient<ICurrencyPriceClient, CurrencyPriceClient>(services, externalApiUrls.CurrencyPriceApiBaseUrl);
        AddCustomClient<IAresClient, AresClient>(services, externalApiUrls.AresEconomicSubjectUrl);
    }

    private static void AddCustomClient<TInterface, TImplementation>(IServiceCollection services, string baseUrl)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });
    }
}
