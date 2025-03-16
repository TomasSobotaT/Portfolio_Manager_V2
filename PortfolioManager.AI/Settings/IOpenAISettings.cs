namespace PortfolioManager.AI.Settings;

public interface IOpenAISettings
{
    string Model { get; }

    string ApiKey { get; }
}
