using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager;

public class AutomapperConfiguration : Profile
{
    public AutomapperConfiguration()
	{
        CreateMap<RegisterUser, UserEntity>();
        CreateMap<UserEntity, UserDto>();
    }
}
