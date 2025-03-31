using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Managers.ToolServices.Interfaces;

public interface ICompanyIdValidationService
{
    Task<DataResult<CompanyIdModel>> ValidateAsync(string text);
}
