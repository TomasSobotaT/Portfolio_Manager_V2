using Microsoft.AspNetCore.Http;
using PortfolioManager.Models.Models.UserFile;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IUserFileService
{
    DataResult<bool> DeleteUserFile(string fileName);

    Task<DataResult<UserFile>> GetUserFileAsync(string fileName);
    
    DataResult<IList<string>> GetUserFilesNames();

    Task<DataResult<string>> SaveUserFileAsync(HttpRequest httpRequest);
}
