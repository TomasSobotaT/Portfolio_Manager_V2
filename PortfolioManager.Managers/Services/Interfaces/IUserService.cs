using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IUserService
{
    Task<DataResult<User>> DeleteUserAsync(int id);

    Task<DataResult<IEnumerable<User>>> GetAllAsync();

    Task<DataResult<User>> GetUserAsync(int id);

    Task<DataResult<User>> UpdateUserAsync(UserEditModel userEditModel, int id);
}
