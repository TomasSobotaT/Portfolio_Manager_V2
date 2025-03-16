using PortfolioManager.Models.Results.Base;

namespace PortfolioManager.Models.Results;

public class DataResult<T> : ErrorResult
{
    public T Data { get; set; }

    public static implicit operator DataResult<T>(T data)
    {
        return new() { Data = data };
    }

    public static implicit operator DataResult<T>(ErrorStatusResult errorResult)
    {
        return new() { Errors = errorResult.Errors, StatusCode = errorResult.StatusCode };
    }
}
