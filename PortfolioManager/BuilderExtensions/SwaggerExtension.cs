using Microsoft.OpenApi.Models;

namespace PortfolioManager.BuilderExtensions;

public static class SwaggerExtension
{
    public static void AddSwaggerOptions(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Portfolio manager API",
                Description = "Webové API pro projekt Portfolio Manager V2 vytvořené pomocí technologie ASP.NET CORE.",
                Contact = new OpenApiContact
                {
                    Name = "Kontakt",
                    Url = new Uri("https://www.tsobota.cz")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer abcdefgh123456\""
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     Array.Empty<string>()
                 }
             });
        });
    }

    public static void UseSwaggerOptions(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio Manager - V2");
        });
    }
}
