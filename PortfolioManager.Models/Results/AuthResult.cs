using PortfolioManager.Models.Results.Base;

namespace PortfolioManager.Models.Results;

public class AuthResult : ErrorResult
{
    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string Token { get; set; }
}
