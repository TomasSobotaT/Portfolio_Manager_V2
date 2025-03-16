namespace PortfolioManager.Base.UserContext;

public interface IUserContext
{
    int GetUserId();

    string GetUserIPAdress();

    string GetUserName();
}
