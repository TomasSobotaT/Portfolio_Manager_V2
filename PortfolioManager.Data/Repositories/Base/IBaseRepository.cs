using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Data.Repositories.Base;

public interface IBaseRepository<TEntity, T> where TEntity :  class, IBaseEntity<T>
{
    Task AddAsync(TEntity entity);

    void Delete(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetAsync(T id);

    void Update(TEntity entity);

    void Commit();

    DbSet<TEntity> GetDbSet();
}
