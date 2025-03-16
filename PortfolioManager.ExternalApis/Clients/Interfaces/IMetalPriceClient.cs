
namespace PortfolioManager.ExternalApis.Clients.Interfaces;

public interface IMetalPriceClient
{
    Task<string> GetMetalPriceAsync();
}
