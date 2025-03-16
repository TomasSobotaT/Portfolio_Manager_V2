using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;

namespace PortfolioManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserFileController(IUserFileService userFileService) : ControllerBase
{
    private readonly IUserFileService userFileService = userFileService;

    [HttpPost("SaveUserFile")]
    [RequestSizeLimit(1_000_000_000)]
    public async Task<IActionResult> SaveUserFileAsync()
    {
        var result = await userFileService.SaveUserFileAsync(this.Request);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetUserFilesNames")]
    public IActionResult GetUserFilesNames()
    {
        var result = userFileService.GetUserFilesNames();
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetUserFile")]
    public async Task<IActionResult> GetUserFileAsync(string fileName)
    {
        var result = await userFileService.GetUserFileAsync(fileName);
        return result.ConvertToFileStreamResult();
    }

    [HttpDelete("DeleteUserFile")]
    public IActionResult DeleteUserFile(string fileName)
    {
        var result = userFileService.DeleteUserFile(fileName);
        return result.ConvertToObjectResult();
    }
}
