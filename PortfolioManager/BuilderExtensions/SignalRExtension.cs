using PortfolioManager.Base.SignalR;

namespace PortfolioManager.BuilderExtensions;

public static class SignalRExtension
{
    public static void UseSignalR(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<UploadHub>("/uploadHub");
        }); ;
    }
}
