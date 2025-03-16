using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Enums;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class UserRoleService(RoleManager<IdentityRole<int>> roleManager, UserManager<UserEntity> userManager) : IUserRoleService
{
    private readonly RoleManager<IdentityRole<int>> roleManager = roleManager;
    private readonly UserManager<UserEntity> userManager = userManager;

    public async Task<DataResult<List<string>>> GetAllRolesAsync()
    {
        var roleEntities = await roleManager.Roles.ToListAsync();

        if (roleEntities is null || roleEntities.Count == 0)
        {
            return new ErrorStatusResult($"No role found", StatusCodes.NotFound);
        }

        return roleEntities.Select(r => r.Name).ToList();
    }

    public async Task<DataResult<bool>> CreateRoleAsync(string roleName)
    {
        if (await roleManager.RoleExistsAsync(roleName))
        {
            return new ErrorStatusResult($"Role {roleName} already exists", StatusCodes.NotFound);
        }

        var result = await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within creating role {roleName}", StatusCodes.BadRequest);
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
                return new ErrorStatusResult($"Error within deleting role {roleName}", StatusCodes.BadRequest);
           }

            return true;
        }

        return new ErrorStatusResult($"Role {roleName} not found", StatusCodes.NotFound);
    }

    public async Task<DataResult<bool>> AssignRoleToUserAsync(int userId, string roleName)
    {
        var userEntity = await userManager.FindByIdAsync(userId.ToString());

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {userId} not found", StatusCodes.NotFound);
        }

        if (await userManager.IsInRoleAsync(userEntity, roleName))
        {
            return new ErrorStatusResult($"User {userEntity.UserName} is already in role {roleName}", StatusCodes.BadRequest);
        }

        var result = await userManager.AddToRoleAsync(userEntity, roleName);

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within assigning role {roleName} to user {userEntity.UserName}", StatusCodes.BadRequest);
        }

        return true;
    }

    public async Task<DataResult<bool>> RemoveRoleFromUserAsync(int userId, string roleName)
    {
        var userEntity = await userManager.FindByIdAsync(userId.ToString());

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {userId} not found", StatusCodes.NotFound);
        }

        if (!await userManager.IsInRoleAsync(userEntity, roleName))
        {
            return new ErrorStatusResult($"User {userEntity.UserName} is not in role {roleName}", StatusCodes.BadRequest);
        }

        var result = await userManager.RemoveFromRoleAsync(userEntity, roleName);

        if (!result.Succeeded)
        {
            return new ErrorStatusResult($"Error within removing role {roleName} from user {userEntity.UserName}", StatusCodes.BadRequest);
        }

        return true;
    }
}

