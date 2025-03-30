using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Transport;
using PortfolioManager.Elasticsearch.Configurations;
using PortfolioManager.Elasticsearch.Repositories.Interfaces;
using PortfolioManager.Models.Models.UserDocument;

namespace PortfolioManager.Elasticsearch.Repositories;

public class ElasticSearchRepository : IElasticSearchRepository
{
    private readonly ElasticsearchClient elasticsearchClient;

    public ElasticSearchRepository(IElasticSearchSettings elasticSearchSettings)
    {
        var cloudNodePool = new CloudNodePool(elasticSearchSettings.CloudId, new ApiKey(elasticSearchSettings.ApiKey));
        var settings = new ElasticsearchClientSettings(cloudNodePool).DefaultIndex(elasticSearchSettings.DefaultIndex);
        elasticsearchClient = new ElasticsearchClient(settings);
    }

    public async Task<bool> IndexDocumentAsync(UserDocumentIndexRequest userDocumentIndexRequest)
    {
        var response = await elasticsearchClient.IndexAsync(userDocumentIndexRequest);
        return response.IsSuccess();
    }

    public async Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string searchText, int userId)
    {
        var response = await elasticsearchClient.SearchAsync<UserDocumentIndexRequest>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                        .Match(ma => ma
                            .Field(f => f.DocumentTextContent)
                            .Query(searchText)
                        )
                    )
                    .Filter(f => f
                        .Term(t => t
                            .Field(ff => ff.UserId)
                            .Value(userId)
                        )
                    )
                )
            )
        );

        if (!response.IsValidResponse)
            return [];

        var results = response.Hits.Select(hit => new UserDocumentSearchResult
        {
            Id = hit.Source.Id,
            FileName = hit.Source.FileName,
            Note = hit.Source.Note,
        }).ToList();

        return results;
    }
}
