using System.Security.Principal;

namespace PortfolioManager.Base.Authentication;

public interface IUserContext
{
    IPrincipal User {  get; }
    int UserId { get; }
}
