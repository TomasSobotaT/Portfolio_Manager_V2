using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PortfolioManager.Base.Entities;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models.User;
using System.Diagnostics.CodeAnalysis;

namespace PortfolioManager.Managers.Managers;

public class AuthManager(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IMapper mapper) : IAuthManager
{
    private readonly SignInManager<UserEntity> signInManager = signInManager;
    private readonly UserManager<UserEntity> userManager = userManager;
    private readonly IMapper mapper = mapper;


    public async Task<UserDto> RegisterUserAsync(RegisterUser registerUser)
    {
        var userEntity = mapper.Map<UserEntity>(registerUser);

        var result = await userManager.CreateAsync(userEntity, registerUser.Password);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(registerUser.Email);
            return mapper.Map<UserDto>(user);
        }

        return null;
    }


    public async Task<UserDto> LoginUserAsync(LoginUser loginUser)
    {
        var result = await signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(loginUser.Email);
            return mapper.Map<UserDto>(user);
        }

        return null;
    }
}
