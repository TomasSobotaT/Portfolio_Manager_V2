using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.Record;
using PortfolioManager.Models.Results;
using System.Net;

namespace PortfolioManager.Managers.Services;

public class RecordService(IRecordRepository recordRepository, ICurrencyRepository currencyRepository, IMapper mapper) : IRecordService
{
    public async Task<DataResult<Record>> GetRecordAsync(int id)
    {
        var recordEntity = await recordRepository.GetAsync(id);
        
        if (recordEntity is null)
        {
            return new ErrorStatusResult($"Record with id {id} not found", HttpStatusCode.NotFound);
        }

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<Record>> AddRecordAsync(RecordEditModel recordEditModel)
    {

        if (recordEditModel is null)
        {
            return new ErrorStatusResult($"Invalid recordEditModel or invalid values");
        }

        var recordEntity = mapper.Map<RecordEntity>(recordEditModel);

        await recordRepository.AddAsync(recordEntity);
        recordRepository.Commit();

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<IList<Record>>> GetAllAsync()
    {
        var recordEntities = await recordRepository.GetAllAsync();

        return mapper.Map<List<Record>>(recordEntities);
    }

    public async Task<DataResult<Record>> DeleteRecordAsync(int id)
    {
        var recordEntity = await recordRepository.GetAsync(id);

        if (recordEntity is null)
        {
            return new ErrorStatusResult($"Record with id {id} not found", HttpStatusCode.NotFound);
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
            return new ErrorStatusResult($"Record with id {id} not found", HttpStatusCode.NotFound);
        }

        mapper.Map(recordEditModel, recordEntity);

        recordRepository.Update(recordEntity);
        recordRepository.Commit();

        return mapper.Map<Record>(recordEntity);
    }

    public async Task<DataResult<IList<UserRecord>>> GetUserRecordsAsync(string currencyName)
    {
        var records = await recordRepository.GetAllAsync();
        var currency = await currencyRepository.GetCurrencyByNameAsync(currencyName);

        if (records is null || !records.Any())
        {
            return new ErrorStatusResult($"No record found", HttpStatusCode.NotFound);
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