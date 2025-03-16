using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities.Base;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;

namespace PortfolioManager.Data.Repositories.Base;

public abstract class ExtendedUserIdRepository<TEntity, T>(ApplicationDbContext applicationDbContext, IUserContext userContext) : ExtendedBaseRepository<TEntity, T>(applicationDbContext, userContext), IExtendedBaseRepository<TEntity, T> where TEntity : UserIdEntity<T> 
{
    private readonly IUserContext userContext = userContext;

    public override async Task AddAsync(TEntity entity)
    {
        entity.UserId = userContext.GetUserId();
        entity.CreatedBy = userContext.GetUserName();
        entity.CreatedDate = DateTime.Now;
        await GetDbSet().AddAsync(entity);
    }

    public override async Task<TEntity> GetAsync(T id)
    {
        var userId = userContext.GetUserId();
        return await GetQueryable().FirstOrDefaultAsync(e => e.Id.Equals(id) && e.UserId.Equals(userId));
    }

    public override async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var userId = userContext.GetUserId();
        return await GetQueryable().Where(e => e.UserId.Equals(userId)).ToListAsync();
    }
}
