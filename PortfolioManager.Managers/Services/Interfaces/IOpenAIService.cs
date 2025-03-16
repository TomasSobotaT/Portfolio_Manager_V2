using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IOpenAIService
{
    Task<DataResult<string>> AskQuestionAsync(string question);
}
