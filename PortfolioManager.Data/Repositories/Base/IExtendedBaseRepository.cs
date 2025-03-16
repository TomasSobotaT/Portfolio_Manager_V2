using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Data.Repositories.Base;

public interface IExtendedBaseRepository<TEntity, T> : IBaseRepository<TEntity, T> where TEntity : class, IExtendedBaseEntity<T>
{
}

