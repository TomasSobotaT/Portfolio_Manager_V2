    using Microsoft.AspNetCore.Mvc;
    using PortfolioManager.Extensions;
    using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Managers.ToolServices.Interfaces;
    using PortfolioManager.Models.Models.User;
using static System.Net.Mime.MediaTypeNames;

    namespace PortfolioManager.BuilderExtensions;

    public static class MinimalApiExtension
    {
        public static void UseMinimalApi(this WebApplication app)
        {
            var authGroup = app.MapGroup("/auth").WithTags("Auth");
            var toolsGroup = app.MapGroup("/tools").WithTags("Tools");

            authGroup.MapPost("/register", async (RegisterUser registerUser, IAuthService authService) =>
            {
                var result = await authService.RegisterUserAsync(registerUser);
                return result.ConvertToResult();
            });

            authGroup.MapPost("/login", async (LoginUser loginUser, IAuthService authService) =>
            {
                var result = await authService.LoginUserAsync(loginUser);
                return result.ConvertToResult();
            });

            authGroup.MapPost("/logout", (IAuthService authService) =>
            {
                var result = authService.Logout();
                return Results.Ok();
            });

            toolsGroup.MapPost("/checkPersonalIdentificationNumber", (IPersonalIdentificationNumberValidationService personalIdentificationNumberValidationService, [FromBodyAttribute] string text) =>
            {
                var result = personalIdentificationNumberValidationService.Validate(text);
                return result.ConvertToResult();
            });

            toolsGroup.MapPost("/checkCompanyId", async (ICompanyIdValidationService companyIdValidationService, [FromBodyAttribute] string text) =>
            {
                var result = await companyIdValidationService.ValidateAsync(text);
                return result.ConvertToResult();
            });
        }
    }
