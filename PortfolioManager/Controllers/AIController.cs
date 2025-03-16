using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AIController(IOpenAIService openAIService, ILogService se) : ControllerBase
{
    private readonly IOpenAIService openAIService = openAIService;

    [HttpPost("AskQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AskQuestionAsync(string question)
    {
        se.LogCustomInformation(question);
        var result = await openAIService.AskQuestionAsync(question);
        return result.ConvertToObjectResult();
    }
}