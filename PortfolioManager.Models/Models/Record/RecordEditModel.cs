using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models.Record;

public class RecordEditModel
{
    public decimal Amount { get; set; }

    public int CommodityId { get; set; }

    [MaxLength(160)]
    public string Note { get; set; }
}
