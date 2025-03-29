using PortfolioManager.Managers.JwtBearer;

namespace PortfolioManager.Middlewares;

public class JwtBlacklistMiddleware(RequestDelegate next, Serilog.ILogger logger)
{
    public async Task Invoke(HttpContext context, IJwtBlackListService jwtBlackListService)
    {
        if (jwtBlackListService.IsTokenBlacklisted())
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token is blacklisted.");
            return;
        }

        await next(context);
    }
}
