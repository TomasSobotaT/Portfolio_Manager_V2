using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PortfolioManager.Base.Entities;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Managers.Interfaces;
using PortfolioManager.Models.Models;

namespace PortfolioManager.Managers.Managers;

public class RecordManager(IRecordRepository recordRepository, ICommodityRepository commodityRepository, IPriceRepository priceRepository, IMapper mapper) : IRecordManager
{
    private readonly IRecordRepository recordRepository = recordRepository;
    private readonly ICommodityRepository commodityRepository = commodityRepository;
    private readonly IPriceRepository priceRepository = priceRepository;

    private readonly IMapper mapper = mapper;


    public async Task<IEnumerable<Record>> GetRecordsAsync(int userId)
    {
        var recordEntities = await recordRepository.GetRecordsAsync(userId);
        var records = await GetRecordsWithPricesAsync(recordEntities);
        return records;
    }

    public async Task<Record> AddRecordAsync(RecordEditModel recordEditModel, int userId)
    {
        var recordEntity = mapper.Map<RecordEntity>(recordEditModel);
        recordEntity.UserId = userId;
        await recordRepository.AddAsync(recordEntity);
        return mapper.Map<Record>(recordEntity);
    }

    private async Task<IEnumerable<Record>> GetRecordsWithPricesAsync(IEnumerable<RecordEntity> records)
    {
        var result = new List<Record>();

        var commodityIds = records.Select(r => r.CommodityId).ToList();
        var commodities = await commodityRepository.GetAllAsync();
        var userCommodities = commodities.Where(c => commodityIds.Contains(c.Id)).ToList();
        var priceIds =  userCommodities.Select(c => c.PriceId).ToList();
        var prices = await priceRepository.GetAllAsync();

        var userPricesDictionary = prices.Where(p => priceIds.Contains(p.Id)).ToDictionary(p => p.Id);
        var userCommoditiesDictionary = userCommodities.ToDictionary(c => c.Id);

        foreach (var recordEntity in records)
        {
            var commodity = userCommoditiesDictionary[recordEntity.CommodityId];
            var price = userPricesDictionary[commodity.PriceId];

            var record = mapper.Map<Record>(recordEntity);
            record.PriceCzck = record.Amount * price.PriceCzk;
            record.PriceUsd = record.Amount * price.PriceUsd;
            result.Add(record);
        }

        return result;
    }
}
