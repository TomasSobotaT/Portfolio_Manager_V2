using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Models.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Record;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class RecordService(IRecordRepository recordRepository, ICurrencyRepository currencyRepository, IMapper mapper) : IRecordService
{
    private readonly IRecordRepository recordRepository = recordRepository;
    private readonly ICurrencyRepository currencyRepository = currencyRepository;
    private readonly IMapper mapper = mapper;

    public async Task<DataResult<Record>> GetRecordAsync(int id)
    {
        var recordEntity = await recordRepository.GetAsync(id);
        
        if (recordEntity is null)
        {
            return new ErrorStatusResult($"Record with id {id} not found", StatusCodes.NotFound);
        }

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<Record>> AddRecordAsync(RecordEditModel recordEditModel)
    {

        if (recordEditModel is null)
        {
            return new ErrorStatusResult($"Invalid recordEditModel or invalid values", StatusCodes.BadRequest);
        }

        var recordEntity = mapper.Map<RecordEntity>(recordEditModel);

        await recordRepository.AddAsync(recordEntity);
        recordRepository.Commit();

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<IEnumerable<Record>>> GetAllAsync()
    {
        var recordEntities = await recordRepository.GetAllAsync();

        return mapper.Map<List<Record>>(recordEntities);
    }

    public async Task<DataResult<Record>> DeleteRecordAsync(int id)
    {
        var recordEntity = await recordRepository.GetAsync(id);

        if (recordEntity is null)
        {
            return new ErrorStatusResult($"Record with id {id} not found", StatusCodes.NotFound);
        }

        recordRepository.Delete(recordEntity);
        recordRepository.Commit();

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<Record>> UpdateRecordAsync(RecordEditModel recordEditModel, int id)
    {
        var recordEntity = await recordRepository.GetAsync(id);

        if (recordEntity is null)
        {
            return new ErrorStatusResult($"Record with id {id} not found", StatusCodes.NotFound);
        }

        mapper.Map(recordEditModel, recordEntity);

        recordRepository.Update(recordEntity);
        recordRepository.Commit();

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<List<UserRecord>>> GetUserRecordsAsync(string currencyName)
    {
        var records = await recordRepository.GetAllAsync();
        var currency = await currencyRepository.GetCurrencyByNameAsync(currencyName);

        if (records is null || !records.Any())
        {
            return new ErrorStatusResult($"No record found", StatusCodes.NotFound);
        }

        var userRecords = new List<UserRecord>();

        foreach (var record in records)
        {
            var userRecord = new UserRecord()
            {
                RecordId = record.Id,
                Amount = record.Amount,
                CommodityName = record.Commodity.Name,
                Currency = currencyName,
                RecordNote = record.Note,
                TotalPrice = record.Amount * record.Commodity.ExchangeRate * currency.ExchangeRate,
            };

            userRecords.Add(userRecord);
        }

        return userRecords;
    }
}