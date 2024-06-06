using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortfolioManager;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers;
using PortfolioManager.Managers.Managers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<UserEntity, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));


builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ILogManager, LogManager>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Portfolio manager API",
        Description = "Webové API pro projekt Portfolio Manager V2 vytvoøené pomocí technologie ASP.NET CORE.",
        Contact = new OpenApiContact
        {
            Name = "Kontakt",
            Url = new Uri("https://www.tsobota.cz")
        }
    });
});

var app = builder.Build();

//var supportedCultures = new[] { new CultureInfo("en-US") };
//app.UseRequestLocalization(new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new RequestCulture("en-US"),
//    SupportedCultures = supportedCultures,
//    SupportedUICultures = supportedCultures
//});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio Manager - v1");
        options.RoutePrefix = string.Empty; // Swagger UI bude dostupný na koøenové URL (https://localhost:<port>/)
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();