namespace PortfolioManager.Base.Entities.Base;

public interface IUserIdEntity<T> : IExtendedBaseEntity<T>
{
    int UserId { get; set; }

    UserEntity User { get; set; }
}
