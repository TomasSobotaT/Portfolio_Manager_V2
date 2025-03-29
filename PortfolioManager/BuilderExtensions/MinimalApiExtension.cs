using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager.BuilderExtensions;

public static class MinimalApiExtension
{
    public static void UseMinimalApi(this WebApplication app)
    {
        var authGroup = app.MapGroup("/Auth").WithTags("Auth");

        var toolsGroup = app.MapGroup("/Tools")
            .WithTags("Tools");
            //.RequireAuthorization();

        authGroup.MapPost("/Register", async (RegisterUser registerUser, IAuthService authService) =>
        {
            var result = await authService.RegisterUserAsync(registerUser);
            return result.ConvertToResult();
        });

        authGroup.MapPost("/Login", async (LoginUser loginUser, IAuthService authService) =>
        {
            var result = await authService.LoginUserAsync(loginUser);
            return result.ConvertToResult();
        });

        authGroup.MapPost("/Logout", (IAuthService authService) =>
        {
            authService.Logout();
            return Results.Ok();
        });

        toolsGroup.MapPost("/CheckPersonalIdentificationNumber", (IPersonalIdentificationNumberValidationService personalIdentificationNumberValidationService, [FromBodyAttribute] string text) =>
        {
            var result = personalIdentificationNumberValidationService.Validate(text);
            return result.ConvertToResult();
        });

        toolsGroup.MapPost("/CheckCompanyId", async (ICompanyIdValidationService companyIdValidationService, [FromBodyAttribute] string text) =>
        {
            var result = await companyIdValidationService.ValidateAsync(text);
            return result.ConvertToResult();
        });
    }
}
