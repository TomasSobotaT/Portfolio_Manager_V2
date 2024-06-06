using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers.Interfaces;
namespace PortfolioManager.Managers.Managers;

public class LogManager(IErrorLogRepository errorLogRepository) : ILogManager
{
    private readonly IErrorLogRepository errorLogRepository = errorLogRepository;

    public void LogError(string ErroMmessage, ErrorTypes errorType = ErrorTypes.BaseError)
    {
        var errorLogEntity = new ErrorLogEntity(ErroMmessage, errorType);
        errorLogEntity.UserId = 1;
        errorLogRepository.AddAsync(errorLogEntity);
    }
}
