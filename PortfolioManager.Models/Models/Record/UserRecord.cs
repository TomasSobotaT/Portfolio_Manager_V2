namespace PortfolioManager.Models.Models.Record;

public class UserRecord
{
    public int RecordId { get; set; }

    public string RecordNote { get; set; }

    public decimal Amount { get; set; }

    public string CommodityName { get; set; }

    public string Currency {  get; set; }

    public decimal TotalPrice { get; set; }
}
