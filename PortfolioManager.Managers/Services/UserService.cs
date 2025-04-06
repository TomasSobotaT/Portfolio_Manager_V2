using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Base.Enums;
using PortfolioManager.Models.Models.User;
using PortfolioManager.Models.Results;
using System.Net;

namespace PortfolioManager.Managers.Services;
public class UserService(UserManager<UserEntity> userManager, IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<DataResult<User>> GetUserAsync(int id)
    {
        var userEntity = await userRepository.GetAsync(id);

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {id} not found", HttpStatusCode.NotFound);
        }

        var roles = await userManager.GetRolesAsync(userEntity);

        var user = mapper.Map<User>(userEntity);
        user.Roles = [.. roles];

        return user;
    }

    public async Task<DataResult<IEnumerable<User>>> GetAllAsync()
    {
        var userEntities = await userRepository.GetAllAsync();

        if (userEntities is null || !userEntities.Any())
        {
            return new ErrorStatusResult($"No user found", HttpStatusCode.NotFound);
        }

        var users =  mapper.Map<List<User>>(userEntities);

        foreach (var user in users)
        {
            var userEntity = userEntities.FirstOrDefault(u => u.Id == user.Id);
            if (userEntity is not null)
            {
                var roles = await userManager.GetRolesAsync(userEntity);
                user.Roles = roles.ToList();
            }
        }

        return users;
    }

    public async Task<DataResult<User>> DeleteUserAsync(int id)
    {
        var userEntity = await userRepository.GetAsync(id);

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {id} not found", HttpStatusCode.NotFound);
        }

        userRepository.Delete(userEntity);
        userRepository.Commit();

        return mapper.Map<User>(userEntity);
    }

    public async Task<DataResult<User>> UpdateUserAsync(UserEditModel userEditModel, int id)
    {
        var userEntity = await userRepository.GetAsync(id);

        if (userEntity is null)
        {
            return new ErrorStatusResult($"User with id {id} not found", HttpStatusCode.NotFound);
        }

        mapper.Map(userEditModel, userEntity);

        userRepository.Update(userEntity);
        userRepository.Commit();

        return mapper.Map<User>(userEntity);
    }
}
