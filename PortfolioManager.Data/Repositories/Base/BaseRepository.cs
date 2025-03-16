using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities.Base;
using PortfolioManager.Data.Context;

namespace PortfolioManager.Data.Repositories.Base;

public abstract class BaseRepository<TEntity, T>(ApplicationDbContext applicationDbContext) : IBaseRepository<TEntity, T> where TEntity : class, IBaseEntity<T>
{
    protected readonly ApplicationDbContext applicationDbContext = applicationDbContext;
    protected readonly DbSet<TEntity> dbSet = applicationDbContext.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await GetDbSet().ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(T id)
    {
        return await GetDbSet().FirstOrDefaultAsync(r => r.Id.Equals(id));
    }

    public virtual void Update(TEntity entity)
    {
        GetDbSet().Update(entity);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await GetDbSet().AddAsync(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        GetDbSet().Remove(entity);
    }

    public void Commit()
    {
        this.applicationDbContext.SaveChanges();
    }

    public virtual DbSet<TEntity> GetDbSet()
    {
        return this.dbSet;
    }

    public virtual IQueryable<TEntity> GetQueryable()
    {
        return GetDbSet();
    }
}
