using PortfolioManager.Base.Enums;

namespace PortfolioManager.Base.Entities;

public class ErrorLogEntity(string errorMessage, ErrorTypes errorType) : BaseEntity
{
    public int UserId { get; set; }

    public ErrorTypes ErrorType { get; set; } = errorType;

    public string ErrorMessage { get; set; } = errorMessage;
}
