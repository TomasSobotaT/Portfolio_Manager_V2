namespace PortfolioManager.Base.Entities.Base;

public class UserIdEntity<T> : ExtendedBaseEntity<T> , IUserIdEntity<T>
{
    public int UserId { get; set; }

    public UserEntity User { get; set; }
}
