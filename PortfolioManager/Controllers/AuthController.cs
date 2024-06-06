using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthManager authManager) : ControllerBase
{
    private readonly IAuthManager authManager = authManager;

    public async Task<IActionResult> RegisterUserAsync(RegisterUser registerUser)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            return BadRequest(new { Errors = errors });
        }

        var result = await authManager.RegisterUserAsync(registerUser);

        if (result == null)
        {
            return BadRequest("Registrace se nezdařila.");
        }

        return Ok(result);
    }
}
