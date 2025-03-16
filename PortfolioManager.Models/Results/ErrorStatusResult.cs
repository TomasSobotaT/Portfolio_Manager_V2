using PortfolioManager.Models.Enums;
using PortfolioManager.Models.Results.Base;

namespace PortfolioManager.Models.Results;

public class ErrorStatusResult : ErrorResult
{
    public ErrorStatusResult(string error, StatusCodes errorStatuCode)
    {
        this.Errors.Add(error);
        this.StatusCode = errorStatuCode;
    }
}
