using PortfolioManager.Base.Entities;

namespace PortfolioManager.Base.Authentication;
public interface ITokenManager
{
    Task<string> GenerateTokenAsync(UserEntity user);
}