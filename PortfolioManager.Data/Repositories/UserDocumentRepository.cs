using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Data.Context;
using PortfolioManager.Data.Repositories.Base;
using PortfolioManager.Data.Repositories.Interfaces;

namespace PortfolioManager.Data.Repositories;

public class UserDocumentRepository(ApplicationDbContext applicationDbContext, IUserContext userContext) : ExtendedUserIdRepository<UserDocumentEntity, int>(applicationDbContext, userContext), IUserDocumentRepository
{
    private readonly IUserContext userContext = userContext;

    public override async Task<IEnumerable<UserDocumentEntity>> GetAllAsync()
    {
        var userId = userContext.GetUserId();
        return await GetQueryable().Where(e => e.UserId == userId)
            .Select(e =>
            new UserDocumentEntity
            {
                Id = e.Id,
                Note = e.Note,
                FileName = e.FileName,
            })
            .ToListAsync();
    }
}
