using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Polly;
using Polly.Retry;

namespace Modulith.Infrastructure.Storage.Azure.Internal;

public sealed class AzureStorage(AzureBlobOption option) : IAzureStorage
{
    private readonly BlobContainerClient _container = new(option.Url, option.Container);

    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<System.Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default) =>
        await _retryPolicy.ExecuteAsync(async () =>
        {
            await _container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
            var newFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var blobClient = _container.GetBlobClient(newFileName);
            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true, cancellationToken);
            return newFileName;
        });

    public async Task RemoveFileAsync(string id, CancellationToken cancellationToken = default) =>
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var blobClient = _container.GetBlobClient(id);
            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots,
                cancellationToken: cancellationToken);
        });
}