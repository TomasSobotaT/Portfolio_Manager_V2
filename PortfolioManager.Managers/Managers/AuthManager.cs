using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models.User;
using System.Diagnostics.CodeAnalysis;

namespace PortfolioManager.Managers.Managers;

public class AuthManager(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IMapper mapper, ILogManager logManager, ITokenManager tokenManager) : IAuthManager
{
    private readonly SignInManager<UserEntity> signInManager = signInManager;
    private readonly UserManager<UserEntity> userManager = userManager;
    private readonly IMapper mapper = mapper;
    private readonly ITokenManager tokenManager = tokenManager;

    public async Task<string> RegisterUserAsync(RegisterUser registerUser)
    {
        var userEntity = mapper.Map<UserEntity>(registerUser);

        var result = await userManager.CreateAsync(userEntity, registerUser.Password);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(registerUser.Email);
            var token =  await tokenManager.GenerateTokenAsync(user);
            return token;
        }

        return null;
    }

    public async Task<string> LoginUserAsync(LoginUser loginUser)
    {
        var users = await userManager.Users.Where(u => u.UserName == loginUser.UserName).ToListAsync();

        foreach (var user in users)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, loginUser.Password, false);
            if (result.Succeeded)
            {
                var token = await tokenManager.GenerateTokenAsync(user);
                return token;
            }
        }

        return null;
    }
}
