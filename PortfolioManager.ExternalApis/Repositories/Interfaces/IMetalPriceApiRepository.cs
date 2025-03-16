
namespace PortfolioManager.ExternalApis.Repositories.Interfaces;

public interface IMetalPriceApiRepository
{
    Task<decimal?> GetMetalPriceAsync(string metalName);
}
