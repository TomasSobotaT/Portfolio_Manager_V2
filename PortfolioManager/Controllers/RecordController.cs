using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Base.Enums;
using PortfolioManager.Managers.Managers;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;
using PortfolioManager.Models.Models.User;
using System.Security.Claims;

namespace PortfolioManager.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RecordController(IRecordManager recordManager, ILogManager logManager) : ControllerBase
{
    private readonly IRecordManager recordManager = recordManager;
    private readonly ILogManager logManager = logManager;


    [HttpGet("Gettest")]
    public async Task<IActionResult> Aaa()
    {
        try
        {
            var response = await recordManager.pokus();

            return Ok(response);
        }
        catch (Exception ex)
        {
            await logManager.LogErrorAsync(ex.Message,"1", ErrorTypes.RecodError);
            return StatusCode(500, new { Message = $"Error within getting record: {ex.Message}" });
        }
    }

    [HttpGet("GetRecords")]
    public async Task<IActionResult> GetRecordsAsync()
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await recordManager.GetRecordsAsync(userId);
            await logManager.LogEventAsync("1", EventTypes.BaseEvent);

            return Ok(response);
        }
        catch (Exception ex)
        {
            await logManager.LogErrorAsync(ex.Message, "1", ErrorTypes.RecodError);
            return StatusCode(500, new { Message = $"Error within getting record: {ex.Message}" });
        }
    }

    [HttpPost("AddRecord")]
    public async Task<IActionResult> AddRecordAsync(RecordEditModel recordEditModel)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await recordManager.AddRecordAsync(recordEditModel, userId);
            await logManager.LogEventAsync("1", EventTypes.BaseEvent);

            return Ok(response);
        }
        catch (Exception ex)
        {
            await logManager.LogErrorAsync(ex.Message, "1", ErrorTypes.RecodError);
            return StatusCode(500, new { Message = $"Error within adding record: {ex.Message}" });
        }
    }
}
