using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Models.Models.File;
using PortfolioManager.Models.Models.UserFile;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Extensions;

public static class ResultExtension
{
    public static IActionResult ConvertToObjectResult<T>(this DataResult<T> result)
    {
        var errorResult = GetErrorResultIfInvalid(result);

        if (errorResult is not null)
        {
            return errorResult;
        }

        result.StatusCode = Models.Enums.StatusCodes.Success;
        return new OkObjectResult(result);
    }

    public static IActionResult ConvertToFileStreamResult(this DataResult<UserDocument> result)
    {
        var errorResult = GetErrorResultIfInvalid(result);

        if (errorResult is not null)
        {
            return errorResult;
        }

        var stream = new MemoryStream(result.Data.FileData);

        return new FileStreamResult(stream, result.Data.ContentType)
        {
            FileDownloadName = result.Data.FileName,
        };
    }

    public static IActionResult ConvertToFileStreamResult(this DataResult<UserFile> result)
    {
        var errorResult = GetErrorResultIfInvalid(result);

        if (errorResult is not null)
        {
            return errorResult;
        }

        var stream = new MemoryStream(result.Data.File);

        return new FileStreamResult(stream, result.Data.ContentType)
        {
            FileDownloadName = result.Data.FileName,
        };
    }

    public static IResult ConvertToResult<T>(this DataResult<T> result)
    {
        var errorResult = GetErrorResultIfInvalid(result);

        if (errorResult is not null)
        {
            if(result.StatusCode == Models.Enums.StatusCodes.NotFound)
            {
                return Results.NotFound(errorResult);

            }

            return Results.BadRequest(errorResult);
        }

        return Results.Ok(result);
    }

    private static IActionResult GetErrorResultIfInvalid<T>(DataResult<T> result)
    {
        if (!result.IsValid)
        {
            return result.StatusCode switch
            {
                Models.Enums.StatusCodes.NotFound => new NotFoundObjectResult(result),
                _ => new BadRequestObjectResult(result),
            };
        }

        return null;
    }
}