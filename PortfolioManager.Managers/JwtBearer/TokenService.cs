using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortfolioManager.Base.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioManager.Managers.JwtBearer
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration configuration = configuration;

        public string GenerateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
