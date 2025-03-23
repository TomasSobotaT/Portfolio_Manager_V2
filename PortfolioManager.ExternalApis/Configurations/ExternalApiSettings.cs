namespace PortfolioManager.ExternalApis.Configurations;

public class ExternalApiSettings : IExternalApiSettings
{
    public string MetalPriceApiBaseUrl { get; set; }

    public string MetalPriceApiSuffix1 { get; set; }

    public string MetalPriceApiSuffix2 { get; set; }

    public string MetalPriceApiKey { get; set; }

    public string CurrencyPriceApiBaseUrl { get; set; }

    public string CurrencyPriceApiSuffix1 { get; set; }

    public string CryptoPriceApiBaseUrl { get; set; }

    public string CryptoPriceApiSuffix1 { get; set; }

    public string CryptoPriceApiSuffix2 { get; set; }

    public string AresEconomicSubjectUrl { get; set; }
}

