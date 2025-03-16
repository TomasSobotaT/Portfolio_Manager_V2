using Hangfire;
using PortfolioManager.Managers.Services.Interfaces;

namespace PortfolioManager.BuilderExtensions;

public static class HangfireExtension
{
    public static void AddHangfire(this IServiceCollection services, string connectionString)
    {
        services.AddHangfire(config => config.UseSqlServerStorage(connectionString));

        services.AddHangfireServer();
    }

    public static void UseHangfire(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<IUpdateDatabaseService>(
            "UpdateDatabase",
            updateDatabaseService => updateDatabaseService.UpdateDatabaseAsync(),
            Cron.Hourly); ;
    }
}
