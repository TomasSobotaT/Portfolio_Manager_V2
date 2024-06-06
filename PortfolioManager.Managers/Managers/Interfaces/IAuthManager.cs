using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers.Interfaces;
public interface IAuthManager
{
    Task<UserDto> RegisterUserAsync(RegisterUser registerUser);
}