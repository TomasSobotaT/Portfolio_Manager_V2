using Microsoft.Extensions.Primitives;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;

namespace PortfolioManager.Models.Models;
public class ErrorLog(string errorMessage, ErrorTypes errorType)
{
    public int? UserId { get; set; }

    public string UserIpAdress { get; set; }

    public ErrorTypes ErrorType { get; set; } = errorType;

    public string ErrorMessage { get; set; } = errorMessage;
}
