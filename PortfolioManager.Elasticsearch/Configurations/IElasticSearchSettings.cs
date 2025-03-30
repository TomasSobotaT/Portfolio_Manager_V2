namespace PortfolioManager.Elasticsearch.Configurations;

public interface IElasticSearchSettings
{
    string Uri { get; set; }

    string CloudId { get; set; }

    string DefaultIndex { get; set; }

    string ApiKey { get; set; }
}
