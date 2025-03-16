using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Extensions;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

//[Authorize(Roles = "admin")]
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService userService = userService;

    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<User>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        var result = await userService.GetUserAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<User>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var result = await userService.DeleteUserAsync(id);
        return result.ConvertToObjectResult();
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<User>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserAsync(UserEditModel userEditModel, int id)
    {
        var result = await userService.UpdateUserAsync(userEditModel, id);
        return result.ConvertToObjectResult();
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataResult<List<User>>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await userService.GetAllAsync();
        return result.ConvertToObjectResult();
    }
}
