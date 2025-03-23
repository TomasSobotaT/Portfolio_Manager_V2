
using PortfolioManager.Models.Responses;

namespace PortfolioManager.ExternalApis.Clients.Interfaces;

public interface IAresClient
{
    Task<AresEconomicSubjectResponse> CheckCompanyIdInAresAsync(string companyId);
}
