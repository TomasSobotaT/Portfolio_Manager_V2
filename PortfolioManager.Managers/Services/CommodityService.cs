using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Models.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Commodity;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class CommodityService(ICommodityRepository commodityRepository, IMapper mapper) : ICommodityService
{
    public async Task<DataResult<Commodity>> GetCommodityAsync(int id)
    {
        var commodityEntity = await commodityRepository.GetAsync(id);
        
        if (commodityEntity is null)
        {
            return new ErrorStatusResult($"Commodity with id {id} not found", StatusCodes.NotFound);
        }

        return mapper.Map<Commodity>(commodityEntity);
    }

    public async Task<DataResult<Commodity>> AddCommodityAsync(CommodityEditModel commodityEditModel)
    {
        if (commodityEditModel is null || string.IsNullOrWhiteSpace(commodityEditModel.Name))
        {
            return new ErrorStatusResult($"Invalid commodityEditModel or invalid name");
        }

        var existingCommodityEntity = await GetCommodityByNameAsync(commodityEditModel.Name);

        if (existingCommodityEntity is not null)
        {
            return new ErrorStatusResult($"Commodity {commodityEditModel.Name} already exists");
        }

        var commodityEntity = mapper.Map<CommodityEntity>(commodityEditModel);
        await commodityRepository.AddAsync(commodityEntity);
        commodityRepository.Commit();

        return mapper.Map<Commodity>(commodityEntity);
    }

    public async Task<DataResult<IList<Commodity>>> GetAllAsync()
    {
        var commodityEntities = await commodityRepository.GetAllAsync();

        return mapper.Map<List<Commodity>>(commodityEntities);
    }

    public async Task<DataResult<Commodity>> DeleteCommodityAsync(int id)
    {
        var commodityEntity = await commodityRepository.GetAsync(id);

        if (commodityEntity is null)
        {
            return new ErrorStatusResult($"Commodity with id {id} not found", StatusCodes.NotFound);
        }

        commodityRepository.Delete(commodityEntity);
        commodityRepository.Commit();

        return mapper.Map<Commodity>(commodityEntity);
    }

    public async Task<DataResult<Commodity>> UpdateCommodityAsync(CommodityEditModel commodityEditModel, int id)
    {
        var commodityEntity = await commodityRepository.GetAsync(id);

        if (commodityEntity is null)
        {
            return new ErrorStatusResult($"Commodity with id {id} not found", StatusCodes.NotFound);
        }

        mapper.Map(commodityEditModel, commodityEntity);

        commodityRepository.Update(commodityEntity);
        commodityRepository.Commit();

        return mapper.Map<Commodity>(commodityEntity);
    }

    private async Task<CommodityEntity> GetCommodityByNameAsync(string name)
    {
        return await commodityRepository.GetCommodityByNameAsync(name);
    }
}
