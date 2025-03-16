using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models.File;
public class UserDocumentEditModel
{
    public IFormFile File { get; set; }

    [MaxLength(50)]
    public string Note { get; set; }
}
