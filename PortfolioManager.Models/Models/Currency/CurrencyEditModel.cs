using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models.Currency;

public class CurrencyEditModel
{
    [MaxLength(20)]
    public string Name { get; set; }

    public decimal ExchangeRate { get; set; }
}
