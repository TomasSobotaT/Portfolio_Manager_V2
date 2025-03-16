using Microsoft.EntityFrameworkCore;
using PortfolioManager.BuilderExtensions;
using PortfolioManager.Configurations;
using PortfolioManager.Data.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Host.AddSerilog(connectionString);

builder.Services.AddHangfire(connectionString);

builder.Services.AddCustomCors();

builder.Services.AddSignalR();

builder.Services.AddHttpClients(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutomapperConfiguration));

builder.Services.AddDependencyInjectionRegistration();

builder.Services.AddSettingsRegistration(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerOptions();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCustomCors();

app.UseSwaggerOptions();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.UseHangfire();

app.UseSignalR();

app.UseMinimalApi();

app.MapControllers();

app.Run();
