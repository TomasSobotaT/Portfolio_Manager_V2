using PortfolioManager.Base.Enums;
using System.Text.Json.Serialization;

namespace PortfolioManager.Base.Entities;

public class CommodityEntity : BaseEntity
{
    public string Name { get; set; }
    public CommodityTypes CommodityType {  get; set; }

    public ICollection<RecordEntity> Records { get; set; }

    public int PriceId { get; set; }

    public PriceEntity Price { get; set; }

}
