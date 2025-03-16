using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

//[Authorize(Roles = "admin")]
[ApiController]
[Route("[controller]")]
public class UserRoleController(IUserRoleService userRoleService) : ControllerBase
{
    private readonly IUserRoleService userRoleService = userRoleService;

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<string>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var result = await userRoleService.GetAllRolesAsync();
        return result.ConvertToObjectResult();
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoleAsync(string roleName)
    {
        var result = await userRoleService.DeleteRoleAsync(roleName);
        return result.ConvertToObjectResult();
    }

    [HttpPost("AssignRoleToUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignRoleToUserAsync(int userId, string roleName)
    {
        var result = await userRoleService.AssignRoleToUserAsync(userId, roleName);
        return result.ConvertToObjectResult();
    }

    [HttpPost("RemoveRoleFromUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<bool>))]
    public async Task<IActionResult> RemoveRoleFromUserAsync(int userId, string roleName)
    {
        var result = await userRoleService.RemoveRoleFromUserAsync(userId, roleName);
        return result.ConvertToObjectResult();
    }

    [HttpPost("CreateRole")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<bool>))]
    public async Task<IActionResult> CreateRoleAsync(string roleName)
    {
        var result = await userRoleService.CreateRoleAsync(roleName);
        return result.ConvertToObjectResult();
    }
}
