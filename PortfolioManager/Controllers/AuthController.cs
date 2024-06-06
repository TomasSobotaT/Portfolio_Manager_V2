using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Controllers;

[Route("api/[controller]")]
public class AuthController(IAuthManager authManager) : ControllerBase
{
    private readonly IAuthManager authManager = authManager;

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUser registerUser)
    {
        
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return BadRequest(new { Errors = errors });
        }
        try
        {
            var result = await authManager.RegisterUserAsync(registerUser);

            if (result == null)
            {
                return BadRequest("Registration failed");
            }

            return Ok(result);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
