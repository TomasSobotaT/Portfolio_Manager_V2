using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.Results.Base;

namespace PortfolioManager.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogService logService)
{
    public async Task Invoke(HttpContext context)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
            logService.LogCustomError("Internal server error", ex);
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(
                new DataResult<ErrorResult>()
                {
                    Data = null,
                    StatusCode = Models.Enums.StatusCodes.InternalError,
                    Errors = ["Internal server error"],
                });

            return; 
        }

    }	
}
