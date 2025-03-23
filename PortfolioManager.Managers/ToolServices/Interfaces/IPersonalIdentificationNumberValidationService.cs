using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Managers.ToolServices.Interfaces;
public interface IPersonalIdentificationNumberValidationService
{
    DataResult<PersonalIdentificationNumber> Validate(string text);
}
