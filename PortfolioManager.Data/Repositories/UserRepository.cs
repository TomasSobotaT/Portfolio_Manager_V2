using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Base;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class UserRepository(ApplicationDbContext applicationDbContext, IUserContext context) : ExtendedBaseRepository<UserEntity, int>(applicationDbContext, context), IUserRepository
{
}
