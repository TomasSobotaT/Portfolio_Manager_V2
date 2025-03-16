using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;
using PortfolioManager.ExternalApis.Services;

namespace PortfolioManager.BuilderExtensions;

public static class HttpClientExtension
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var externalApiUrls = configuration.GetSection("ExternalApiSettings").Get<ExternalApiSettings>();

        services.AddHttpClient<ICryptoPriceClient, CryptoPriceClient>(client =>
        {
            client.BaseAddress = new Uri(externalApiUrls.CryptoPriceApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        services.AddHttpClient<IMetalPriceClient, MetalPriceClient>(client =>
        {
            client.BaseAddress = new Uri(externalApiUrls.MetalPriceApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        services.AddHttpClient<ICurrencyPriceClient, CurrencyPriceClient>(client =>
        {
            client.BaseAddress = new Uri(externalApiUrls.CurrencyPriceApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });
    }
}
