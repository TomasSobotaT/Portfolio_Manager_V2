using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService authService = authService;

    [HttpPost("Register")]  
    public async Task<IActionResult> RegisterUserAsync(RegisterUser registerUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResult { Errors = GetErrorList(ModelState) });
        }

         var result = await authService.RegisterUserAsync(registerUser);

         if (!result.IsValid)
         {
             return BadRequest(result);
         }

         return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginUser loginUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResult { Errors = GetErrorList(ModelState) });
        }

        var result = await authService.LoginUserAsync(loginUser);

        if (!result.IsValid)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> LogOutUserAsync()
    {
        await authService.LogOutUserAsync();
        return Ok();
    }

    private static List<string> GetErrorList(ModelStateDictionary modelstate)
    {
        return modelstate.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
    }
}
