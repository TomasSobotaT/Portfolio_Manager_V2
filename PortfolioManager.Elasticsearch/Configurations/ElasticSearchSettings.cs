namespace PortfolioManager.Elasticsearch.Configurations;

public class ElasticSearchSettings : IElasticSearchSettings
{
    public string Uri { get; set; }

    public string CloudId { get; set; }

    public string DefaultIndex { get; set; }

    public string ApiKey { get; set; }
}
