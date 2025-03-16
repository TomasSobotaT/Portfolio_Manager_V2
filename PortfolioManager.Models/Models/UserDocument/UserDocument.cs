namespace PortfolioManager.Models.Models.File;

public class UserDocument
{
    public int Id { get; set; }

    public string Note { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] FileData { get; set; }
}
