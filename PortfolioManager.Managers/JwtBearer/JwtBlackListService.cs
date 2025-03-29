using Microsoft.Extensions.Caching.Memory;
using PortfolioManager.Base.UserContext;

namespace PortfolioManager.Managers.JwtBearer;

public class JwtBlackListService(IUserContext userContext, IMemoryCache cache) : IJwtBlackListService
{
    public void BlackListJwttoken()
    {
        var token = userContext.GetToken();
        
        if (token is not null)
        {
            cache.Set(token, true, TimeSpan.FromHours(24));
        }
    }

    public bool IsTokenBlacklisted()
    {
        var token = userContext.GetToken();

        if (token is null)
        {
            return false;
        }

        return cache.TryGetValue(token, out _);
    }
}
