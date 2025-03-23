using PortfolioManager.ExternalApis.Clients.Interfaces;
using PortfolioManager.ExternalApis.Configurations;
using PortfolioManager.Models.Responses;
using System.Text.Json;

namespace PortfolioManager.ExternalApis.Clients;

public class AresClient(HttpClient httpClient, IExternalApiSettings externalApiSettings) : IAresClient
{
    public async Task<AresEconomicSubjectResponse> CheckCompanyIdInAresAsync(string companyId)
    {
        var url = $"{externalApiSettings.AresEconomicSubjectUrl}{companyId}";

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AresEconomicSubjectResponse>(content);
    }
}
