using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.Base.Enums;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models.User;
using System.Net;

namespace PortfolioManager.Controllers
{
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
                    await LogEventAsync(EventTypes.RegisterFailedEvent);
                    return BadRequest(new { Message = "Registration failed" });
                }

                await LogEventAsync(EventTypes.RegisterSuccessEvent);
                return Ok(new { Token = result });
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex.Message, ErrorTypes.RegisterError);
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
                    await LogEventAsync(EventTypes.LoginFailedEvent);
                    return Unauthorized(new { Message = "Invalid login attempt" });
                }

                await LogEventAsync(EventTypes.LoginSuccessEvent);
                return Ok(new { Token = result });
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex.Message, ErrorTypes.LoginError);
                return StatusCode(500, new { Message = $"Error within login: {ex.Message}" });
            }
        }

        private async Task LogEventAsync(EventTypes eventType)
        {
            var ipAddress = GetIpAddress();
            await logManager.LogEventAsync(ipAddress, eventType);
        }

        private async Task LogErrorAsync(string message, ErrorTypes errorType)
        {
            var ipAddress = GetIpAddress();
            await logManager.LogErrorAsync(message, ipAddress, errorType);
        }

        private string GetIpAddress()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress is null)
            {
                return "Unknown";
            }

            if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            }

            return remoteIpAddress.ToString();
        }
    }
}