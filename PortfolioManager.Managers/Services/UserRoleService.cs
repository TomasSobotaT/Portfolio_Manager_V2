using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Base.Enums;
using PortfolioManager.Models.Results;
using System.Net;

namespace PortfolioManager.Managers.Services;

public class UserRoleService(RoleManager<IdentityRole<int>> roleManager, UserManager<UserEntity> userManager) : IUserRoleService
{
    public async Task<DataResult<List<string>>> GetAllRolesAsync()
    {
        var roleEntities = await roleManager.Roles.ToListAsync();

        if (roleEntities is null || roleEntities.Count == 0)
        {
            return new ErrorStatusResult($"No role found", HttpStatusCode.NotFound);
        }

        return roleEntities.Select(r => r.Name).ToList();
    }

    public async Task<DataResult<bool>> CreateRoleAsync(string roleName)
    {
        if (await roleManager.RoleExistsAsync(roleName))
        {
            return new ErrorStatusResult($"Role {roleName} already exists", HttpStatusCode.NotFound);
        }

        var result = await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within creating role {roleName}");
        }

        return true;
    }

    public async Task<DataResult<bool>> DeleteRoleAsync(string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role is not null)
        {
            var result = await roleManager.DeleteAsync(role);
           if (!result.Succeeded)
           {
                return new ErrorStatusResult($"Error within deleting role {roleName}");
           }

            return true;
        }

        return new ErrorStatusResult($"Role {roleName} not found", HttpStatusCode.NotFound);
    }

    public async Task<DataResult<bool>> AssignRoleToUserAsync(int userId, string roleName)
    {
        var userEntity = await userManager.FindByIdAsync(userId.ToString());

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {userId} not found", HttpStatusCode.NotFound);
        }

        if (await userManager.IsInRoleAsync(userEntity, roleName))
        {
            return new ErrorStatusResult($"User {userEntity.UserName} is already in role {roleName}");
        }

        var result = await userManager.AddToRoleAsync(userEntity, roleName);

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within assigning role {roleName} to user {userEntity.UserName}");
        }

        return true;
    }

    public async Task<DataResult<bool>> RemoveRoleFromUserAsync(int userId, string roleName)
    {
        var userEntity = await userManager.FindByIdAsync(userId.ToString());

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {userId} not found", HttpStatusCode.NotFound);
        }

        if (!await userManager.IsInRoleAsync(userEntity, roleName))
        {
            return new ErrorStatusResult($"User {userEntity.UserName} is not in role {roleName}");
        }

        var result = await userManager.RemoveFromRoleAsync(userEntity, roleName);

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within removing role {roleName} from user {userEntity.UserName}");
        }

        return true;
    }
}

