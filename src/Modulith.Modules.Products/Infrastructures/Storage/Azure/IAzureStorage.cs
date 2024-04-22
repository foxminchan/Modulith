using Microsoft.AspNetCore.Http;

namespace Modulith.Modules.Products.Infrastructures.Storage.Azure;

public interface IAzureStorage
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task RemoveFileAsync(string id, CancellationToken cancellationToken = default);
}