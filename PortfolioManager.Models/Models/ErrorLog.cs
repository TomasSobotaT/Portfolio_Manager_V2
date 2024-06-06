using Microsoft.Extensions.Primitives;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;

namespace PortfolioManager.Models.Models;
public class ErrorLog
{
    public int UserId { get; set; }

    public ErrorTypes ErrorType { get; set; }

    public string ErrorMessage { get; set; }    
}
