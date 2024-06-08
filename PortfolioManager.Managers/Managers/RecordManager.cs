using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers;

public class RecordManager(IRecordRepository recordRepository) : IRecordManager
{
    private readonly IRecordRepository recordRepository = recordRepository;

    public Task<IEnumerable<Record>> GetUserRecordsAsync()
    {
        return null;
    }
}
