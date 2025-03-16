using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager.BuilderExtensions;

public static class MinimalApiExtension
{
    public static void UseMinimalApi(this WebApplication app)
    {
        app.MapPost("/Register", async (RegisterUser registerUser, IAuthService authService) =>
        {
            var result = await authService.RegisterUserAsync(registerUser);
            return result;
        });

        app.MapPost("/Login", async (LoginUser loginUser, IAuthService authService) =>
        {
            var result = await authService.LoginUserAsync(loginUser);
            return result;
        });

        app.MapPost("/Logout", async (IAuthService authService) =>
        {
            //TODO: token blacklist v cache
        });
    }
}
