using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models.User;

public class UserDto
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
