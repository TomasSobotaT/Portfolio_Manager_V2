namespace PortfolioManager.Base.Entities;

public class RecordEntity : BaseEntity
{
    public UserEntity User { get; set; }

    public int UserId {  get; set; }

    public decimal Amount { get; set; }

    public int CommodityId { get; set; }

    public CommodityEntity Commodity { get; set; }
}
