namespace PortfolioManager.Base.Entities.Base;

public abstract class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; }

    public bool IsDeleted { get; set; }
}
