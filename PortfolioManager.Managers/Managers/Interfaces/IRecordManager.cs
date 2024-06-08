using PortfolioManager.Base.Entities;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers.Interfaces;

public interface IRecordManager
{
    Task<Record> AddRecordAsync(RecordEditModel recordEditModel, int userId);
    Task<IEnumerable<Record>> GetRecordsAsync(int userId);
}
