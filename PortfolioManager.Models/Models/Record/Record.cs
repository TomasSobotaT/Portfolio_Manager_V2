namespace PortfolioManager.Models.Models.Record;

public class Record : RecordEditModel
{
    public int Id { get; set; }

    public Commodity.Commodity Commodity { get; set; }
}
