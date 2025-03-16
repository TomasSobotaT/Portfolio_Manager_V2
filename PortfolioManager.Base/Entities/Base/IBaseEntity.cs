namespace PortfolioManager.Base.Entities.Base;

public interface IBaseEntity<T>
{
    T Id { get; set; }

    bool IsDeleted { get; set; }
}
