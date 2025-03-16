namespace PortfolioManager.BuilderExtensions;

public static class CorsExtension
{
    public static void AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsConfig", builder =>
            {
                builder.WithOrigins("https://www.fe.cz")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
    }

    public static void UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("CorsConfig");
    }
}
