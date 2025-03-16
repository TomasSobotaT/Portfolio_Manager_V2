using PortfolioManager.Base.Entities;

namespace PortfolioManager.Managers.JwtBearer;

public interface ITokenService
{
    string GenerateToken(UserEntity user);
}
