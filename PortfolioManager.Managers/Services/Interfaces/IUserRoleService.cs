using Microsoft.AspNetCore.Identity;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IUserRoleService
{
    Task<DataResult<bool>> AssignRoleToUserAsync(int userId, string roleName);

    Task<DataResult<bool>> CreateRoleAsync(string roleName);

    Task<DataResult<bool>> DeleteRoleAsync(string roleName);

    Task<DataResult<List<string>>> GetAllRolesAsync();
   
    Task<DataResult<bool>> RemoveRoleFromUserAsync(int userId, string roleName);
}
