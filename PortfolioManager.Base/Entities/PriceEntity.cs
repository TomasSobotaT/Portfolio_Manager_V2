﻿namespace PortfolioManager.Base.Entities;

public class PriceEntity : BaseEntity
{
    public decimal PriceCzk { get; set; }

    public decimal PriceUsd { get; set; }

    public ICollection<CommodityEntity> Commodity { get; set; }
}
