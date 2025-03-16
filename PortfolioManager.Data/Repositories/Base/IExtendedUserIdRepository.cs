using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Data.Repositories.Base;

public interface IExtendedUserIdRepository<TEntity, T> : IExtendedBaseRepository<TEntity, T> where TEntity : class, IUserIdEntity<T>
{
}
