using OpenAI.Chat;
using PortfolioManager.AI.Repositories.Interfaces;
using PortfolioManager.AI.Settings;

namespace PortfolioManager.AI.Repositories;

public class OpenAIRepository(IOpenAISettings openAISettings) : IOpenAIRepository
{
    private readonly string apiKey = openAISettings.ApiKey;
    private readonly string model = openAISettings.Model;

    public async Task<string>GenerateAnswerAsync(string question)
    {
        var client = new ChatClient(model, apiKey);

        var completion =  await client.CompleteChatAsync(question);
        return completion.Value.Content[0].Text;
    }
}