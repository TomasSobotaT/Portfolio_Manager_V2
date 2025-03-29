namespace PortfolioManager.Managers.JwtBearer;

public interface IJwtBlackListService
{
    void BlackListJwttoken();

    bool IsTokenBlacklisted();
}
