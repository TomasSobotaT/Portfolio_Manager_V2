using PortfolioManager.Models.Models.UserDocument;

namespace PortfolioManager.Elasticsearch.Repositories.Interfaces;

public interface IElasticSearchRepository
{
    Task IndexDocumentAsync(UserDocumentIndexRequest entity);

    Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string query);
}
