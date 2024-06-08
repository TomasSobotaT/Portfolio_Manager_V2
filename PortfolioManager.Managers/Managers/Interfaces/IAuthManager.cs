using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Managers.Managers.Interfaces;
public interface IAuthManager
{
    Task<string> LoginUserAsync(LoginUser loginUser);
    Task<string> RegisterUserAsync(RegisterUser registerUser);
}