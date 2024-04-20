namespace Modulith.Infrastructure.Storage.Azure;

public sealed class AzureBlobOption
{
    public string Url { get; set; } = string.Empty;
    public string Container { get; set; } = string.Empty;
}