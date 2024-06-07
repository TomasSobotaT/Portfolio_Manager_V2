using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Managers.Managers.Interfaces;
public interface IAuthManager
{
    Task<UserDto> LoginUserAsync(LoginUser loginUser);
    Task<UserDto> RegisterUserAsync(RegisterUser registerUser);
}