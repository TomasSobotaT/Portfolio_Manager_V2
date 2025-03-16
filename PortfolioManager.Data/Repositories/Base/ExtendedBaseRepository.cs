using PortfolioManager.Base.Entities.Base;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;

namespace PortfolioManager.Data.Repositories.Base;

public abstract class ExtendedBaseRepository<TEntity, T>(ApplicationDbContext applicationDbContext, IUserContext context) : BaseRepository<TEntity, T>(applicationDbContext), IExtendedBaseRepository<TEntity,T> where TEntity : class, IExtendedBaseEntity<T>
{
    public override void Update(TEntity entity)
    {
        entity.UpdatedBy = context.GetUserName();
        entity.UpdatedDate = DateTime.Now;
        GetDbSet().Update(entity);
    }

    public override async Task AddAsync(TEntity entity)
    {
        entity.CreatedBy = context.GetUserName();
        entity.CreatedDate = DateTime.Now;
        await GetDbSet().AddAsync(entity);
    }

    public override void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }
}
