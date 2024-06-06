using System.ComponentModel.DataAnnotations;

namespace PortfolioManager.Models.Models;
public class RegisterUser
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
    public string PasswordConfirm { get; set; }
}
