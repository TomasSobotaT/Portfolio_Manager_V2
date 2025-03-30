using PortfolioManager.Models.Models.UserDocument;

namespace PortfolioManager.Elasticsearch.Repositories.Interfaces;

public interface IElasticSearchRepository
{
    Task<bool> IndexDocumentAsync(UserDocumentIndexRequest userDocumentIndexRequest);

    Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string query, int userId);
}
