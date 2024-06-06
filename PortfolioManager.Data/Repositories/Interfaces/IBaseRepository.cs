using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);

    Task DeleteAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(int id);

    Task UpdateAsync(T entity);
}