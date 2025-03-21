using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
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
            return result.ConvertToResult();
        });

        app.MapPost("/Login", async (LoginUser loginUser, IAuthService authService) =>
        {
            var result = await authService.LoginUserAsync(loginUser);
            return result.ConvertToResult();
        });

        app.MapPost("/Logout", async (IAuthService authService) =>
        {
            //TODO: token blacklist v cache
        });

        app.MapPost("/CheckPersonalIdentificationNumber", (IPersonalIdentificationNumberValidationService personalIdentificationNumberValidationService, [FromBodyAttribute]string text) =>
        {
           var result = personalIdentificationNumberValidationService.Validate(text);
           return result.ConvertToResult();
        })
        .WithTags("Tools");
    }
}
