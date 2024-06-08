using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortfolioManager.Base.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioManager.Base.Authentication;
public class TokenManager(IConfiguration configuration, UserManager<UserEntity> userManager) : ITokenManager
{
    private readonly IConfiguration configuration = configuration;
    private readonly UserManager<UserEntity> userManager = userManager;

    public async Task<string> GenerateTokenAsync(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
