using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;

public class UserDocumentEntity : UserIdEntity<int>
{
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public string Note {  get; set; }

    public byte[] FileData { get; set; }
}
