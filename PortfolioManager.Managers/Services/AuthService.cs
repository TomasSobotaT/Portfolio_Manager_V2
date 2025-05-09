﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Managers.JwtBearer;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class AuthService(UserManager<UserEntity> userManager, IMapper mapper, ITokenService tokenService, ILogService logService, IJwtBlackListService jwtBlackListService, IUserContext userContext) : IAuthService
{
    public async Task<DataResult<AuthResult>> RegisterUserAsync(RegisterUser registerUser)
    {
        var userEntity = mapper.Map<UserEntity>(registerUser);

        var result = await userManager.CreateAsync(userEntity, registerUser.Password);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(registerUser.UserName);

            var token = tokenService.GenerateToken(user);

            return new AuthResult
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                Token = token
            };
        }

        return new ErrorStatusResult("Registration failed"){ Errors = result.Errors.Select(e => e.Description).ToList()};
    }

    public async Task<DataResult<AuthResult>> LoginUserAsync(LoginUser loginUser)
    {
        if (string.IsNullOrWhiteSpace(loginUser.Name) || string.IsNullOrWhiteSpace(loginUser.Password))
        {
            return new ErrorStatusResult("Username and password must be filled");
        }

        var userEntity = await userManager.FindByNameAsync(loginUser.Name);
        
        if (userEntity is null)
        {
            return new ErrorStatusResult($"Username {loginUser.Name} not found");
        }

        var result = await userManager.CheckPasswordAsync(userEntity, loginUser.Password);

        if (result)
        {
            var token = tokenService.GenerateToken(userEntity);
            LogUserInfo(userEntity);
            return new AuthResult
            { 
                UserName = userEntity.UserName,
                UserEmail = userEntity.Email,
                Token = token
            };
        }

        return new ErrorStatusResult("Login failed");
    }
    
    public void Logout()
    {
        jwtBlackListService.BlackListJwttoken();
    }

    private void LogUserInfo(UserEntity userEntity)
    {
        var ipAddress = userContext.GetUserIPAdress();
        logService.LogCustomInformation($"User logged. UserId = {userEntity.Id}, UserName = {userEntity.UserName}, IP Address = {ipAddress}");
    }
}
