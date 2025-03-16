using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;

public class CurrencyEntity : ExtendedBaseEntity<int>
{
    public string Name { get; set; }

    public decimal ExchangeRate { get; set; }
}
