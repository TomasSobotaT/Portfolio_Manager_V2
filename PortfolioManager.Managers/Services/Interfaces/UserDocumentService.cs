using PortfolioManager.Models.Models.File;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IUserDocumentService
{
    Task<DataResult<UserDocumentOutputModel>> DeleteUserDocumentAsync(int id);

    Task<DataResult<UserDocument>> GetUserDocumentAsync(int id);

    Task<DataResult<IList<UserDocumentOutputModel>>> GetUserDocumentsOverviewAsync();

    Task<DataResult<bool>> SaveUserDocumentAsync(UserDocumentEditModel userDocumentEditModel);
}
