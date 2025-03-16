namespace PortfolioManager.AI.Repositories.Interfaces;

public interface IOpenAIRepository
{
    Task<string> GenerateAnswerAsync(string question);
}
