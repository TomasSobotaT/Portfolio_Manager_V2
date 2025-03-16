namespace PortfolioManager.Base.Entities.Base;

public interface IExtendedBaseEntity<T> : IBaseEntity<T>
{
    DateTime CreatedDate { get; set; }

    DateTime? UpdatedDate { get; set; }

    string CreatedBy { get; set; }

    string UpdatedBy { get; set; }
}
