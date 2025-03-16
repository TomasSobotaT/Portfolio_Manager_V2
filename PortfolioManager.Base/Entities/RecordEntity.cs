using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;

public class RecordEntity : UserIdEntity<int>
{
    public decimal Amount { get; set; }

    public string Note { get; set; }

    public int CommodityId { get; set; }

    public CommodityEntity Commodity { get; set; }
}
