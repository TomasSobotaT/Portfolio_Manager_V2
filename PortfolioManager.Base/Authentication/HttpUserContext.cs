using Microsoft.AspNetCore.Http;
using PortfolioManager.Base.Entities;
using System.Security.Claims;
using System.Security.Principal;

namespace PortfolioManager.Base.Authentication;
public class HttpUserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    public IPrincipal User
    {
        get
        {
            return httpContextAccessor.HttpContext?.User;

        }
    }

    public int UserId
    {
        get
        {
            var userIdClaim = (httpContextAccessor.HttpContext?.User)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            return 1;
        }
    }
}
