namespace PortfolioManager.Models.Models.UserDocument;

public class UserDocumentIndexRequest
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public string Note { get; set; }

    public string DocumentTextContent { get; set; } 
}
