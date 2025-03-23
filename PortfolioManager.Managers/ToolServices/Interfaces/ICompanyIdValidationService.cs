using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Managers.ToolServices.Interfaces;

public interface ICompanyIdValidationService
{
    Task<DataResult<CompanyId>> ValidateAsync(string text);
}
