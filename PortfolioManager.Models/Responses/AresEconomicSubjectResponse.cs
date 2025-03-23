using System.Text.Json.Serialization;

namespace PortfolioManager.Models.Responses;

public class AresEconomicSubjectResponse
{
    [JsonPropertyName("ico")]
    public string CompanyId { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public string CompanyName { get; set; }

    [JsonPropertyName("dic")]
    public string Vatid { get; set; }

    [JsonPropertyName("sidlo")]
    public CompanyAddress CompanyAddress { get; set; }
}

public class CompanyAddress
{
    [JsonPropertyName("textovaAdresa")]
    public string Address { get; set; }
}