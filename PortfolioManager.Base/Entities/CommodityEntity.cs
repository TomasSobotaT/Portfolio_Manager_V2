using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;

public class CommodityEntity : ExtendedBaseEntity<int>
{
    public string Name { get; set; }

    public decimal ExchangeRate { get; set; }

    public string Note {  get; set; }
}
