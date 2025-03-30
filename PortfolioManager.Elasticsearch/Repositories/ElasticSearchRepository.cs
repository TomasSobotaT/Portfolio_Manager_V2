using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using PortfolioManager.Elasticsearch.Configurations;
using PortfolioManager.Elasticsearch.Repositories.Interfaces;
using PortfolioManager.Models.Models.UserDocument;
using System.Net.Http.Headers;

namespace PortfolioManager.Elasticsearch.Repositories;

public class ElasticSearchRepository(IElasticSearchSettings elasticSearchSettings) : IElasticSearchRepository
{
    public async Task IndexDocumentAsync(UserDocumentIndexRequest userDocumentIndexRequest)
    {
        var pool = new CloudNodePool(elasticSearchSettings.CloudId, new ApiKey(elasticSearchSettings.ApiKey));

        var settings = new ElasticsearchClientSettings(pool)
            .DefaultIndex("userdocuments");

        var client = new ElasticsearchClient(settings);

        var response = await client.IndexAsync(userDocumentIndexRequest);
    }

    public Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string query)
    {
        throw new NotImplementedException();
    }
}
