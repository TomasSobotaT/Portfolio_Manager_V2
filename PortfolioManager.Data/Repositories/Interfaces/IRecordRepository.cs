using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Repositories.Interfaces;

public interface IRecordRepository : IBaseRepository<RecordEntity>
{
    Task<IEnumerable<RecordEntity>> GetRecordsAsync(int userId);
}
