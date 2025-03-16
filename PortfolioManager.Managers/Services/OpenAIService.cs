using PortfolioManager.AI.Repositories.Interfaces;
using PortfolioManager.Models.Enums;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class OpenAIService(IOpenAIRepository openAIRepository) : IOpenAIService
{
    public async Task<DataResult<string>> AskQuestionAsync(string question)
    {
		try
		{
            return await openAIRepository.GenerateAnswerAsync(question);
        }

		catch (Exception ex)
		{
            return new ErrorStatusResult($"Error within communication with OpenAI Api : {ex.Message}", StatusCodes.BadRequest);
		}
    }
}
