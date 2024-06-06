using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Managers.Managers.Interfaces;
public interface IAuthManager
{
    Task<UserDto> RegisterUserAsync(RegisterUser registerUser);
}