using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IAuthService
{
    Task<DataResult<AuthResult>> LoginUserAsync(LoginUser loginUser);

    Task<DataResult<AuthResult>> RegisterUserAsync(RegisterUser registerUser);
}