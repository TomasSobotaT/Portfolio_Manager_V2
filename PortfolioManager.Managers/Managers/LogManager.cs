using AutoMapper;
using PortfolioManager.Base.Authentication;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers;

public class LogManager(IErrorLogRepository errorLogRepository, IEventLogRepository eventLogRepository, IUserContext userContext, IMapper mapper) : ILogManager
{
    private readonly IErrorLogRepository errorLogRepository = errorLogRepository;
    private readonly IEventLogRepository eventLogRepository = eventLogRepository;
    private readonly IUserContext userContext = userContext;
    private readonly IMapper mapper = mapper;

    public async Task LogErrorAsync(string ErroMmessage, string userIpAdress, ErrorTypes errorType = ErrorTypes.BaseError)
    {
        var errorLog = new ErrorLog(ErroMmessage, errorType);
        var errorLogEntity = mapper.Map<ErrorLogEntity>(errorLog);
        errorLogEntity.UserId = userContext.UserId;
        errorLogEntity.UserIpAdress = userIpAdress;
        await errorLogRepository.AddAsync(errorLogEntity);
    }

    public async Task LogEventAsync(string userIpAdress, EventTypes evetType = EventTypes.BaseEvent)
    {
        var eventLog = new EventLog(evetType);
        var eventLogEntity = mapper.Map<EventLogEntity>(eventLog);
        eventLogEntity.UserId = userContext.UserId;
        eventLogEntity.UserIpAdress = userIpAdress;
        await eventLogRepository.AddAsync(eventLogEntity);
    }
}
