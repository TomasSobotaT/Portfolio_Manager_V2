using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Record;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class RecordController(IRecordService recordService) : ControllerBase
{
    private readonly IRecordService recordService = recordService;

    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Record>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecordAsync(int id)
    {
        var result = await recordService.GetRecordAsync(id);
        return result.ConvertToObjectResult();
    }
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Record>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRecordAsync(RecordEditModel recordEditModel)
    {
        var result = await recordService.AddRecordAsync(recordEditModel);
        return result.ConvertToObjectResult();
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Record>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecordAsync(int id)
    {
        var result = await recordService.DeleteRecordAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Record>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRecordAsync(RecordEditModel recordEditModel, int id)
    {
        var result = await recordService.UpdateRecordAsync(recordEditModel, id);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<Record>>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await recordService.GetAllAsync();
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetAllUserRecords")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<UserRecord>>))]
    public async Task<IActionResult> GetAllUserRecordAsync(string currencyName)
    {
        var result = await recordService.GetUserRecordsAsync(currencyName);
        return result.ConvertToObjectResult();
    }
}
