using Microsoft.AspNetCore.Identity;
namespace PortfolioManager.Base.Entities;

public class UserEntity : IdentityUser<int>
{
    public ICollection<RecordEntity> Records { get; set; }

    public bool IsDeleted { get; set; }
}
