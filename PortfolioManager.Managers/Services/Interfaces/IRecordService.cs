﻿using PortfolioManager.Models.Models.Record;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services.Interfaces;

public interface IRecordService
{
    Task<DataResult<Record>> AddRecordAsync(RecordEditModel recordEditModel);

    Task<DataResult<Record>> DeleteRecordAsync(int id);

    Task<DataResult<IEnumerable<Record>>> GetAllAsync();

    Task<DataResult<Record>> GetRecordAsync(int id);

    Task<DataResult<List<UserRecord>>> GetUserRecordsAsync(string currencyName);

    Task<DataResult<Record>> UpdateRecordAsync(RecordEditModel RecordEditModel, int id);
}
