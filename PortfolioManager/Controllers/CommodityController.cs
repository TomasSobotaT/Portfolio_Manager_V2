using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Commodity;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CommodityController(ICommodityService commodityService) : ControllerBase
{
    private readonly ICommodityService commodityService = commodityService;

    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Commodity>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCommodityAsync(int id)
    {
        var result = await commodityService.GetCommodityAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Commodity>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCommodityAsync(CommodityEditModel commodityEditModel)
    {
        var result = await commodityService.AddCommodityAsync(commodityEditModel);
        return result.ConvertToObjectResult();
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Commodity>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCommodityAsync(int id)
    {
        var result = await commodityService.DeleteCommodityAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Commodity>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCommodityAsync(CommodityEditModel commodityEditModel, int id)
    {
        var result = await commodityService.UpdateCommodityAsync(commodityEditModel, id);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<Commodity>>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await commodityService.GetAllAsync();
        return result.ConvertToObjectResult();
    }
}
