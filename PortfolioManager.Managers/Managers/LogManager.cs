using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers.Interfaces;

namespace PortfolioManager.Managers.Managers;

public class LogManager(IErrorLogRepository errorLogRepository, IUserContext userContext) : ILogManager
{
    private readonly IErrorLogRepository errorLogRepository = errorLogRepository;
    private readonly IUserContext userContext = userContext;


    public async Task LogErrorAsync(string ErroMmessage, ErrorTypes errorType = ErrorTypes.BaseError)
    {
        var errorLogEntity = new ErrorLogEntity(ErroMmessage, errorType);
        errorLogEntity.UserId = userContext.UserId;
        await errorLogRepository.AddAsync(errorLogEntity);
    }
}
