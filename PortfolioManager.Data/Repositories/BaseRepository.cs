using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class BaseRepository<T>(ApplicationDbContext applicationDbContext, IUserContext userContext) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext applicationDbContext = applicationDbContext;
    protected readonly IUserContext userContext = userContext;
    protected readonly DbSet<T> dbSet = applicationDbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.Where(r => !r.IsDeleted).ToListAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        return await dbSet.Where(r => r.Id == id && !r.IsDeleted).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedBy = userContext?.User?.Identity?.Name;
        entity.UpdatedDate = DateTime.Now;
        dbSet.Update(entity);
        await SaveChangesToDatabaseAsync();
    }

    public async Task AddAsync(T entity)
    {
        entity.UpdatedBy = userContext?.User?.Identity?.Name;
        entity.CreatedBy = userContext?.User?.Identity?.Name;
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        await dbSet.AddAsync(entity);
        await SaveChangesToDatabaseAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity is not null)
        {
            entity.UpdatedBy = userContext?.User?.Identity?.Name;
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
        await SaveChangesToDatabaseAsync();
    }

    private async Task SaveChangesToDatabaseAsync()
    {
        await this.applicationDbContext.SaveChangesAsync();
    }

}
