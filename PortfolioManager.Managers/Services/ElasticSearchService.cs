using PortfolioManager.Elasticsearch.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.UserDocument;

namespace PortfolioManager.Managers.Services;

public class ElasticSearchService(IElasticSearchRepository elasticSearchRepository) : IElasticSearchService
{
    public async Task IndexDocumentAsync(UserDocumentIndexRequest userDocumentSearchRequest)
    {
        await elasticSearchRepository.IndexDocumentAsync(userDocumentSearchRequest);
    }

    public async Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string query)
    {
        return await elasticSearchRepository.SearchDocumentsAsync(query);
    }
}
