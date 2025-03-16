using AutoMapper;
using Microsoft.AspNetCore.Http;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Helpers;
using PortfolioManager.Data.Repositories;
using PortfolioManager.Data.Repositories.Interfaces;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.File;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class UserDocumentService(IUserDocumentRepository userDocumentRepository, IMapper mapper) : IUserDocumentService
{
    private readonly IUserDocumentRepository userDocumentRepository = userDocumentRepository;
    private readonly IMapper mapper = mapper;

    private const int MaxFileSize = 10_000_000;

    public async Task<DataResult<bool>> SaveUserDocumentAsync(UserDocumentEditModel userDocumentEditModel)
    {
        var checkResult = CheckUserDocument(userDocumentEditModel);

        if (checkResult is not null)
        {
            return checkResult;
        }

        try
        {
            var userDocumentEntity = new UserDocumentEntity
            {
                FileName = userDocumentEditModel.File.FileName,
                ContentType = userDocumentEditModel.File.ContentType,
                Note = userDocumentEditModel.Note,
                FileData = await GetByteArrayFromFileAsync(userDocumentEditModel.File),
            };

            await userDocumentRepository.AddAsync(userDocumentEntity);
            userDocumentRepository.Commit();

            return true;
        }
        catch (Exception ex)
        {
            return new ErrorStatusResult($"Error whithin saving file: {ex.Message}", Models.Enums.StatusCodes.BadRequest);
        }
    }

    public async Task<DataResult<UserDocument>> GetUserDocumentAsync(int id)
    {
        var userDocumentEntity = await userDocumentRepository.GetAsync(id);
          
        if (userDocumentEntity is null || userDocumentEntity.FileData is null)
        {
            return new ErrorStatusResult($"File with id {id} not found", Models.Enums.StatusCodes.NotFound);
        }

        return mapper.Map<UserDocument>(userDocumentEntity);
    }

    public async Task<DataResult<List<UserDocumentOutputModel>>> GetUserDocumentsOverviewAsync()
    {
        var userDocumentEntities = await userDocumentRepository.GetAllAsync();

        if (userDocumentEntities is null || !userDocumentEntities.Any())
        {
            return new ErrorStatusResult($"No file found", Models.Enums.StatusCodes.NotFound);
        }

        var userDocumentOutputModels = mapper.Map<List<UserDocumentOutputModel>>(userDocumentEntities);
        return userDocumentOutputModels;
    }

    public async Task<DataResult<UserDocumentOutputModel>> DeleteUserDocumentAsync(int id)
    {
        var userDocumentEntitity = await userDocumentRepository.GetAsync(id);

        if (userDocumentEntitity is null)
        {
            return new ErrorStatusResult($"No file found", Models.Enums.StatusCodes.NotFound);
        }

        userDocumentRepository.Delete(userDocumentEntitity);
        userDocumentRepository.Commit();

        var userDocumentOutputModels = mapper.Map<UserDocumentOutputModel>(userDocumentEntitity);
        return userDocumentOutputModels;
    }

    private static async Task<byte[]> GetByteArrayFromFileAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    private static ErrorStatusResult CheckUserDocument(UserDocumentEditModel userDocumentEditModel)
    {
        if (userDocumentEditModel is null || userDocumentEditModel.File is null || userDocumentEditModel.File.Length == 0)
        {
            return new ErrorStatusResult("No file to upload.", Models.Enums.StatusCodes.BadRequest);
        }

        if (userDocumentEditModel.File.Length > MaxFileSize)
        {
            return new ErrorStatusResult("File size exceeds 10 MB.", Models.Enums.StatusCodes.BadRequest);
        }


        if (string.IsNullOrWhiteSpace(Path.GetExtension(userDocumentEditModel.File.FileName)))
        {
            return new ErrorStatusResult("File has no extension.", Models.Enums.StatusCodes.BadRequest);
        }

        var fileName = FileHelper.NormalizeFileName(userDocumentEditModel.File.FileName);

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return new ErrorStatusResult("Bad file name.", Models.Enums.StatusCodes.BadRequest);
        }

        return null;
    }
}
