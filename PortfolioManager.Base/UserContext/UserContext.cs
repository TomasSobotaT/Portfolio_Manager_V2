using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PortfolioManager.Base.UserContext;

public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor contextAccessor = contextAccessor;

    public string GetUserName()
    {
        return contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? "System";
    }

    public int GetUserId()
    {
        var userIdString =  contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        throw new InvalidOperationException("User Id could not be retrieved from the current HTTP context.");
    }

    public string GetUserIPAdress()
    {
        var forwardedHeader = contextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrEmpty(forwardedHeader))
        {
            return forwardedHeader;
        }

        return contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}
