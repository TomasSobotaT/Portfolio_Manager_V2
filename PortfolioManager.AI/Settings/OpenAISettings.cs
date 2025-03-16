namespace PortfolioManager.AI.Settings;

public class OpenAISettings : IOpenAISettings
{
    public string Model { get; set; }

    public string ApiKey {  get; set; }
}
