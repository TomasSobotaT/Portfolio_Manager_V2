using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Repositories.Base;

namespace PortfolioManager.Data.Repositories.Interfaces;

public interface IRecordRepository : IExtendedUserIdRepository<RecordEntity, int>
{
}
