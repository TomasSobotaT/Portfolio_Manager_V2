using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IAuthService
{
    void Logout();

    Task<DataResult<AuthResult>> LoginUserAsync(LoginUser loginUser);

    Task<DataResult<AuthResult>> RegisterUserAsync(RegisterUser registerUser);
}