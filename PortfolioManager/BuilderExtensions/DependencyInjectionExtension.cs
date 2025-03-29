using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Data.Repositories;
using PortfolioManager.Managers.JwtBearer;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Managers.Services;
using PortfolioManager.Base.UserContext;
using PortfolioManager.ExternalApis.Configurations;
using PortfolioManager.ExternalApis.Repositories.Interfaces;
using PortfolioManager.ExternalApis.Repositories;
using PortfolioManager.AI.Repositories.Interfaces;
using PortfolioManager.AI.Repositories;
using PortfolioManager.AI.Settings;
using PortfolioManager.Managers.Configurations;
using PortfolioManager.Managers.Tools;
using PortfolioManager.Managers.ToolServices.Interfaces;
using PortfolioManager.Managers.ToolServices;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PortfolioManager.BuilderExtensions;

public static class DependencyInjectionExtension
{
    public static void AddDependencyInjectionRegistration(this IServiceCollection services)
    {
        services.TryAddScoped<IAuthService, AuthService>();
        services.TryAddScoped<ICommodityService, CommodityService>();
        services.TryAddScoped<ITokenService, TokenService>();
        services.TryAddScoped<ICurrencyService, CurrencyService>();
        services.TryAddScoped<IRecordService, RecordService>();
        services.TryAddScoped<IUpdateDatabaseService, UpdateDatabaseService>();
        services.TryAddScoped<IUpdateDatabaseService, UpdateDatabaseService>();
        services.TryAddScoped<IOpenAIService, OpenAIService>();
        services.TryAddScoped<IUserDocumentService, UserDocumentService>();
        services.TryAddScoped<IUserFileService, UserFileService>();
        services.TryAddScoped<IMailService, MailService>();
        services.TryAddScoped<ILogService, LogService>();
        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<IUserRoleService, UserRoleService>();
        services.TryAddScoped<IPersonalIdentificationNumberValidationService, PersonalIdentificationNumberValidationService>();
        services.TryAddScoped<ICompanyIdValidationService, CompanyIdValidationService>();
        services.TryAddScoped<IJwtBlackListService, JwtBlackListService>();

        services.TryAddScoped<ICommodityRepository, CommodityRepository>();
        services.TryAddScoped<ICurrencyRepository, CurrencyRepository>();
        services.TryAddScoped<IRecordRepository, RecordRepository>();
        services.TryAddScoped<IMetalPriceApiRepository, MetalPriceApiRepository>();
        services.TryAddScoped<ICryptoPriceApiRepository, CryptoPriceApiRepository>();
        services.TryAddScoped<ICurrencyPriceApiRepository, CurrencyPriceApiRepository>();
        services.TryAddScoped<IOpenAIRepository, OpenAIRepository>();
        services.TryAddScoped<IUserDocumentRepository, UserDocumentRepository>();
        services.TryAddScoped<IUserRepository, UserRepository>();

        services.TryAddScoped<IUserContext, UserContext>();
    }

    public static void AddSettingsRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var externalApiSettings = new ExternalApiSettings();
        configuration.GetSection("ExternalApiSettings").Bind(externalApiSettings);
        services.AddSingleton<IExternalApiSettings>(externalApiSettings);

        var openAISettings = new OpenAISettings();
        configuration.GetSection("OpenAISettings").Bind(openAISettings);
        services.AddSingleton<IOpenAISettings>(openAISettings);

        var mailISettings = new MailSettings();
        configuration.GetSection("MailSettings").Bind(mailISettings);
        services.AddSingleton<IMailSettings>(mailISettings);
    }
}
