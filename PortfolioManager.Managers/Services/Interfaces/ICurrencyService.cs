using PortfolioManager.Models.Models.Currency;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface ICurrencyService
{
    Task<DataResult<Currency>> AddCurrencyAsync(CurrencyEditModel currencyEditModel);

    Task<DataResult<Currency>> DeleteCurrencyAsync(int id);

    Task<DataResult<IEnumerable<Currency>>> GetAllAsync();

    Task<DataResult<Currency>> GetCurrencyAsync(int id);

    Task<DataResult<Currency>> UpdateCurrencyAsync(CurrencyEditModel CurrencyEditModel, int id);
}
