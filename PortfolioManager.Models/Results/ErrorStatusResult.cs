using PortfolioManager.Models.Results.Base;
using System.Net;

namespace PortfolioManager.Models.Results;

public class ErrorStatusResult : ErrorResult
{
    public ErrorStatusResult(string error, HttpStatusCode errorStatuCode = HttpStatusCode.BadRequest)
    {
        this.Errors.Add(error);
        this.StatusCode = errorStatuCode;
    }
}
