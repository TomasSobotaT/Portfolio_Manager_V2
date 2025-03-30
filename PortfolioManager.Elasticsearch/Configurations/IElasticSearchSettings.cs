namespace PortfolioManager.Elasticsearch.Configurations;

public interface IElasticSearchSettings
{
    string Url { get; set; }

    string CloudId { get; set; }

    string DefaultIndex { get; set; }

    string ApiKey { get; set; }
}
