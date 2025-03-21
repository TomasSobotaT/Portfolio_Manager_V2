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
using PortfolioManager.Managers.Services.Tools;

namespace PortfolioManager.BuilderExtensions;

public static class DependencyInjectionExtension
{
    public static void AddDependencyInjectionRegistration(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICommodityService, CommodityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IRecordService, RecordService>();
        services.AddScoped<IUpdateDatabaseService, UpdateDatabaseService>();
        services.AddScoped<IUpdateDatabaseService, UpdateDatabaseService>();
        services.AddScoped<IOpenAIService, OpenAIService>();
        services.AddScoped<IUserDocumentService, UserDocumentService>();
        services.AddScoped<IUserFileService, UserFileService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IPersonalIdentificationNumberValidationService, PersonalIdentificationNumberValidationService>();

        services.AddScoped<ICommodityRepository, CommodityRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IRecordRepository, RecordRepository>();
        services.AddScoped<IMetalPriceApiRepository, MetalPriceApiRepository>();
        services.AddScoped<ICryptoPriceApiRepository, CryptoPriceApiRepository>();
        services.AddScoped<ICurrencyPriceApiRepository, CurrencyPriceApiRepository>();
        services.AddScoped<IOpenAIRepository, OpenAIRepository>();
        services.AddScoped<IUserDocumentRepository, UserDocumentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserContext, UserContext>();
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
