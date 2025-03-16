using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using PortfolioManager.Base.Helpers;
using PortfolioManager.Base.SignalR;
using PortfolioManager.Base.UserContext;
using PortfolioManager.Managers.Services.Interfaces;
using PortfolioManager.Models.Models.UserFile;
using PortfolioManager.Models.Results;

namespace PortfolioManager.Managers.Services;

public class UserFileService(IWebHostEnvironment webHostEnvironment, IUserContext userContext, IHubContext<UploadHub> uploadHubContext) : IUserFileService
{
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
    private readonly IUserContext userContext = userContext;
    private readonly IHubContext<UploadHub> uploadHubContext = uploadHubContext;

    public async Task<DataResult<string>> SaveUserFileAsync(HttpRequest httpRequest)
    {
        var userId = userContext.GetUserId();

        if (!httpRequest.Query.TryGetValue("originalFileSize", out var originalFileSizeValue) ||
            !long.TryParse(originalFileSizeValue.ToString().Trim(), out var originalFileSize))
        {
            return new ErrorStatusResult("Requested upload is not valid.", Models.Enums.StatusCodes.BadRequest);
        }

        if (!httpRequest.HasFormContentType ||
            !MediaTypeHeaderValue.TryParse(httpRequest.ContentType, out var mediaTypeHeader) ||
            string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
        {
            return new ErrorStatusResult("Requested upload is not valid", Models.Enums.StatusCodes.BadRequest);
        }

        var boundaryValue = HeaderUtilities.RemoveQuotes(mediaTypeHeader.Boundary.Value).Value;

        if (boundaryValue is null)
        {
            return new ErrorStatusResult("Request boundary is not valid", Models.Enums.StatusCodes.BadRequest);
        }

        var tempFileName = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-")
                           + Random.Shared.Next().ToString("D10")
                           + ".tmp";

        var tempFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", "Temp", tempFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));

        string originalFileName = null;
        string finalFilePath = null;
        long totalBytesRead = 0;

        var reader = new MultipartReader(boundaryValue, httpRequest.Body);

        while (true)
        {
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
            {
                break;
            }

            if (ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition) &&
                contentDisposition.DispositionType.Equals("form-data", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(contentDisposition.FileName.Value))
            {
                originalFileName = FileHelper.NormalizeFileName(contentDisposition.FileName.Value);

                if (string.IsNullOrWhiteSpace(originalFileName))
                {
                    return new ErrorStatusResult("File has bad or no name", Models.Enums.StatusCodes.BadRequest);
                }

                finalFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", $"{userId}", originalFileName);

                if (File.Exists(finalFilePath))
                {
                    return new ErrorStatusResult($"File {originalFileName} already exists", Models.Enums.StatusCodes.BadRequest);
                }

                using var targetStream = File.Create(tempFilePath);
                var buffer = new byte[81920];
                int bytesRead;

                while ((bytesRead = await section.Body.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await targetStream.WriteAsync(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    var progress = (double)totalBytesRead / originalFileSize * 100;

                    await uploadHubContext.Clients.User(userId.ToString()).SendAsync("ReceiveUploadProgress", progress);
                }
            }
        }

        if (originalFileName is null || totalBytesRead != originalFileSize)
        {
            if (tempFilePath != null) File.Delete(tempFilePath);
            return new ErrorStatusResult("File upload failed", Models.Enums.StatusCodes.BadRequest);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(finalFilePath));

        File.Move(tempFilePath, finalFilePath);

        return originalFileName;
    }

    public DataResult<IList<string>> GetUserFilesNames()
    {
        var userId = userContext.GetUserId();

        var userDirectoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", $"{userId}");

        if (!Directory.Exists(userDirectoryPath))
        {
            return new ErrorStatusResult($"User has no files", Models.Enums.StatusCodes.NotFound);
        }

        var userFiles =  Directory.GetFiles(userDirectoryPath);

        if (userFiles is null || userFiles.Length == 0)
        {
            return new ErrorStatusResult($"User has no files", Models.Enums.StatusCodes.NotFound);
        }

        return userFiles.Select(Path.GetFileName).ToList();
    }

    public async Task<DataResult<UserFile>> GetUserFileAsync(string fileName)
    {
        var userId = userContext.GetUserId();

        var userDirectoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", $"{userId}");

        if (!Directory.Exists(userDirectoryPath))
        {
            return new ErrorStatusResult($"User has no files", Models.Enums.StatusCodes.NotFound);
        }

        var userFilePath = Path.Combine(userDirectoryPath, fileName);

        if (!File.Exists(userFilePath))
        {
            return new ErrorStatusResult($"File '{fileName}' not found", Models.Enums.StatusCodes.NotFound);
        }

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(userFilePath, out var contentType))
        {
            contentType = "application/octet-stream"; 
        }

        var fileContent = await File.ReadAllBytesAsync(userFilePath);

        return new UserFile {File = fileContent, ContentType = contentType, FileName = fileName };
    }

    public DataResult<bool> DeleteUserFile(string fileName)
    {
        var userId = userContext.GetUserId();

        var userDirectoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", $"{userId}");

        if (!Directory.Exists(userDirectoryPath))
        {
            return new ErrorStatusResult($"User has no files", Models.Enums.StatusCodes.NotFound);
        }

        var userFilePath = Path.Combine(userDirectoryPath, fileName);

        if (File.Exists(userFilePath))
        {
            try
            {
               File.Delete(userFilePath);
            }
            catch (Exception ex)
            {
                return new ErrorStatusResult($"Error within deleting file {fileName}: {ex.Message}", Models.Enums.StatusCodes.BadRequest);
            }
        }

        else
        {
            return new ErrorStatusResult($"File {fileName} not found", Models.Enums.StatusCodes.NotFound);
        }

        return true;
    }
}
