using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models.User;
public class RegisterUser
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string PasswordConfirm { get; set; }
}
