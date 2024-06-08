using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Base.Enums;
using PortfolioManager.Managers.Managers;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthManager authManager, ILogManager logManager) : ControllerBase
{
    private readonly IAuthManager authManager = authManager;
    private readonly ILogManager logManager = logManager;


    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUser registerUser)
    {
        try
        {
            var result = await authManager.RegisterUserAsync(registerUser);
            if (result is null)
            {
                return BadRequest("Registration failed");
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            await logManager.LogErrorAsync(ex.Message,ErrorTypes.RegisterError);
            return StatusCode(500, new { Message = $"Error within registration: {ex.Message}" });
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        try
        {
            var result = await authManager.LoginUserAsync(loginUser);
            if (result is null)
            {
                return Unauthorized(new { Message = "Invalid login attempt" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            await logManager.LogErrorAsync(ex.Message, ErrorTypes.LoginError);
            return StatusCode(500, new { Message = $"Error within login: {ex.Message}" });
        }
    }
}
