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
        var userIdString = contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        throw new InvalidOperationException("User Id could not be retrieved from the current HTTP context.");
    }

    public string GetUserIPAdress()
    {
        var ipAddress = contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

        if (!string.IsNullOrWhiteSpace(ipAddress))
        {
            return ipAddress;
        }

        return contextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault() ?? "Unknown";
    }

    public string GetToken()
    {
        var authorizationHeader = contextAccessor.HttpContext?.Request?.Headers?.Authorization.FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader["Bearer ".Length..].Trim();
        }

        return null;
    }
}
