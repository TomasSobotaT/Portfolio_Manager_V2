using PortfolioManager.Models.Models.UserDocument;

namespace PortfolioManager.Managers.Services.Interfaces;
public interface IElasticSearchService
{
    Task IndexDocumentAsync(UserDocumentIndexRequest userDocumentSearchRequest);

    Task<List<UserDocumentSearchResult>> SearchDocumentsAsync(string searchText, int userId);
}
