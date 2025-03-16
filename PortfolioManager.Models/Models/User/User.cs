namespace PortfolioManager.Models.Models.User;

public class User : UserEditModel
{
    public int Id { get; set; }

    public List<string> Roles { get; set; }
}
