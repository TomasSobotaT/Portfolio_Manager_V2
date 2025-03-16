namespace PortfolioManager.Base.Entities.Base;

public abstract class ExtendedBaseEntity<T> : BaseEntity<T>, IExtendedBaseEntity<T>
{
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
}