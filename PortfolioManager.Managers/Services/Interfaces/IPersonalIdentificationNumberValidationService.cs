using PortfolioManager.Models.Models.PersonalIdentificationNumber;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;
public interface IPersonalIdentificationNumberValidationService
{
    DataResult<PersonalIdentificationNumber> Validate(string text);
}
