namespace PortfolioManager.Base.UserContext;

public interface IUserContext
{
    string GetToken();

    int GetUserId();

    string GetUserIPAdress();

    string GetUserName();
}
