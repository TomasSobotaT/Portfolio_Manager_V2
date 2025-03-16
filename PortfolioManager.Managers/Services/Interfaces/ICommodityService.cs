using PortfolioManager.Models.Models.Commodity;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface ICommodityService
{
    Task<DataResult<Commodity>> AddCommodityAsync(CommodityEditModel commodityEditModel);

    Task<DataResult<Commodity>> DeleteCommodityAsync(int id);

    Task<DataResult<IEnumerable<Commodity>>> GetAllAsync();

    Task<DataResult<Commodity>> GetCommodityAsync(int id);

    Task<DataResult<Commodity>> UpdateCommodityAsync(CommodityEditModel commodityEditModel, int id);
}
