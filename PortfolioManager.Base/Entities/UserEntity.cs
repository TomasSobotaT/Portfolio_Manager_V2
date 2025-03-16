using Microsoft.AspNetCore.Identity;
using PortfolioManager.Base.Entities.Base;

namespace PortfolioManager.Base.Entities;

public class UserEntity : IdentityUser<int>, IExtendedBaseEntity<int>
{
    public ICollection<RecordEntity> Records { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
}
