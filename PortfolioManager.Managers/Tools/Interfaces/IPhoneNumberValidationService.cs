using PortfolioManager.Base.Enums;
using PortfolioManager.Models.Results;
using PortfolioManager.Models.ToolModels;

namespace PortfolioManager.Managers.Tools.Interfaces;

public interface IPhoneNumberValidationService
{
    DataResult<PhoneNumberModel> Validate(string text, Countries country);
}
