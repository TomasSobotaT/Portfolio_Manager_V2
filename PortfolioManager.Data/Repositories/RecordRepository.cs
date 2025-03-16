using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Base;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class RecordRepository(ApplicationDbContext applicationDbContext, IUserContext context) : ExtendedUserIdRepository<RecordEntity, int>(applicationDbContext, context), IRecordRepository
{
    public override IQueryable<RecordEntity> GetQueryable()
    {
        return this.applicationDbContext.Records.Include(r => r.Commodity);
    }
}
