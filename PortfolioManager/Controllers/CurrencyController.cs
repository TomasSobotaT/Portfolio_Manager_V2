using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Currency;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    private readonly ICurrencyService currencyService = currencyService;

    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Currency>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrencyAsync(int id)
    {
        var result = await currencyService.GetCurrencyAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Currency>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCurrencyAsync(CurrencyEditModel currencyEditModel)
    {
        var result = await currencyService.AddCurrencyAsync(currencyEditModel);
        return result.ConvertToObjectResult();
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Currency>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCurrencyAsync(int id)
    {
        var result = await currencyService.DeleteCurrencyAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<Currency>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCurrencyAsync(CurrencyEditModel currencyEditModel, int id)
    {
        var result = await currencyService.UpdateCurrencyAsync(currencyEditModel, id);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<Currency>>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await currencyService.GetAllAsync();
        return result.ConvertToObjectResult();
    }
}
