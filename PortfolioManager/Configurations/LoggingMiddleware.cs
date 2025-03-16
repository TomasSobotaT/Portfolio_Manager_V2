namespace PortfolioManager.Configurations;

public class LoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
{
    private readonly RequestDelegate next = next;
    private readonly Serilog.ILogger logger = logger;

    public async Task Invoke(HttpContext context)
    {
        string userId = "Unknown";
        string userName = "Unknown";

        if (context.User?.Identity?.IsAuthenticated == true)
        {
            userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
            userName = context.User.Identity?.Name ?? "Unknown";
        }

        var loggerWithUserId = logger.ForContext("UserId", userId)
                                      .ForContext("UserName", userName)
                                      .ForContext("SourceContext", "LoggingMiddleware");

        loggerWithUserId.Information("HTTP {Method} {Path} responded from User {UserName}",
            context.Request.Method,
            context.Request.Path,
            userName);

        await next(context);
    }
}
