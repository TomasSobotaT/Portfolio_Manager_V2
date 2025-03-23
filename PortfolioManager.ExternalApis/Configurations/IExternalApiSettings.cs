namespace PortfolioManager.ExternalApis.Configurations;

public interface IExternalApiSettings
{
    string MetalPriceApiBaseUrl { get; }

    string MetalPriceApiSuffix1 { get; }

    string MetalPriceApiSuffix2 { get; }

    string MetalPriceApiKey{ get; }

    string CurrencyPriceApiBaseUrl { get; }

    string CurrencyPriceApiSuffix1 { get; }

    string CryptoPriceApiBaseUrl { get; }

    string CryptoPriceApiSuffix1 { get; }

    string CryptoPriceApiSuffix2 { get; }

    string AresEconomicSubjectUrl { get; }
}
