using Microsoft.AspNetCore.Identity;
using PortfolioManager.Base.Entities;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers;

public class AuthManager : IAuthManager
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserManager<UserEntity> userManager;


    public async Task<UserDto> RegisterUserAsync(RegisterUser registerUser)
    {
        var userEntity = new UserEntity
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
        };

        var result = await userManager.CreateAsync(userEntity, registerUser.Password);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(registerUser.Email);
            var registeredUser = new UserDto { UserName = user.UserName, Email = user.Email };
            //var token = tokenService.GenerateToken(userDto);
            return registeredUser;
        }

        return null;
    }
}
