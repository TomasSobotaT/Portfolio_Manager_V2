using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> LoginUserAsync(LoginUser loginUser);

    Task LogOutUserAsync();

    Task<AuthResult> RegisterUserAsync(RegisterUser registerUser);
}