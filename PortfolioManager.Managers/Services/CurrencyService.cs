using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Currency;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper) : ICurrencyService
{
    public async Task<DataResult<Currency>> GetCurrencyAsync(int id)
    {
        var currencyEntity = await currencyRepository.GetAsync(id);
        
        if (currencyEntity is null)
        {
            return new ErrorStatusResult($"Currency with id {id} not found", StatusCodes.NotFound);
        }

        return mapper.Map<Currency>(currencyEntity);
    }

    public async Task<DataResult<Currency>> AddCurrencyAsync(CurrencyEditModel currencyEditModel)
    {
        if(currencyEditModel is null || string.IsNullOrWhiteSpace(currencyEditModel.Name))
        {
            return new ErrorStatusResult($"Invalid currencyEditModel or invalid name");
        }

        var existingCurrencyEntity = await GetCurrencyByNameAsync(currencyEditModel.Name);

        if (existingCurrencyEntity is not null)
        {
            return new ErrorStatusResult($"Currency {currencyEditModel.Name} already exists");
        }

        var CurrencyEntity = mapper.Map<CurrencyEntity>(currencyEditModel);
        await currencyRepository.AddAsync(CurrencyEntity);
        currencyRepository.Commit();

        return mapper.Map<Currency>(CurrencyEntity);
    }

    public async Task<DataResult<IList<Currency>>> GetAllAsync()
    {
        var CurrencyEntities = await currencyRepository.GetAllAsync();

        return mapper.Map<List<Currency>>(CurrencyEntities);
    }

    public async Task<DataResult<Currency>> DeleteCurrencyAsync(int id)
    {
        var currencyEntity = await currencyRepository.GetAsync(id);

        if (currencyEntity is null)
        {
            return new ErrorStatusResult($"Currency with id {id} not found");
        }

        currencyRepository.Delete(currencyEntity);
        currencyRepository.Commit();

        return mapper.Map<Currency>(currencyEntity);
    }

    public async Task<DataResult<Currency>> UpdateCurrencyAsync(CurrencyEditModel CurrencyEditModel, int id)
    {
        var currencyEntity = await currencyRepository.GetAsync(id);

        if (currencyEntity is null)
        {
            return new ErrorStatusResult($"Currency with id {id} not found");
        }

        mapper.Map(CurrencyEditModel, currencyEntity);

        currencyRepository.Update(currencyEntity);
        currencyRepository.Commit();

        return mapper.Map<Currency>(currencyEntity);
    }

    private async Task<CurrencyEntity> GetCurrencyByNameAsync(string name)
    {
        return await currencyRepository.GetCurrencyByNameAsync(name);
    }
}
