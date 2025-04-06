using System.Net;

namespace PortfolioManager.Models.Results.Base;

public abstract class ErrorResult
{
    public List<string> Errors { get; set; } = [];

    public bool IsValid => Errors is null || Errors.Count == 0;

    public HttpStatusCode StatusCode { get; set; }
}
