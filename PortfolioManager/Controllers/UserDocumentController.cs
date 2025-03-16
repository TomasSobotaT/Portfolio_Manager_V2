using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.File;

namespace PortfolioManager.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class UserDocumentController(IUserDocumentService userDocumentService) : ControllerBase
{
    private readonly IUserDocumentService userDocumentService = userDocumentService;

    [HttpPost("SaveUserDocument")]
    public async Task<IActionResult> SaveUserDocumentAsync(UserDocumentEditModel userDocumentEditModel)
    {
        var result = await userDocumentService.SaveUserDocumentAsync(userDocumentEditModel);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetUserDocument")]
    public async Task<IActionResult> GetUserDocumentAsync(int id)
    {
        var result = await userDocumentService.GetUserDocumentAsync(id);
        return result.ConvertToFileStreamResult();
    }

    [HttpGet("GetUserDocumentsOverview")]
    public async Task<IActionResult> GetUserDocumentsOverviewAsync()
    {
        var result = await userDocumentService.GetUserDocumentsOverviewAsync();
        return result.ConvertToObjectResult();
    }

    [HttpDelete("DeleteUserDocument")]
    public async Task<IActionResult> DeleteUserDocumentAsync(int id)
    {
        var result = await userDocumentService.DeleteUserDocumentAsync(id);
        return result.ConvertToObjectResult();
    }
}
