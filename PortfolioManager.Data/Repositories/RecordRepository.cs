using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class RecordRepository(ApplicationDbContext applicationDbContext, IUserContext userContext)
    : BaseRepository<RecordEntity>(applicationDbContext, userContext), IRecordRepository
{
    public async Task<IEnumerable<RecordEntity>> GetRecordsAsync(int userId)
    {
       return await dbSet.Where(r => r.UserId == userId).ToListAsync();
    }
}
