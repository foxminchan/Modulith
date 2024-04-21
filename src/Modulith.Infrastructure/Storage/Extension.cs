using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Infrastructure.Storage.Azure;
using Modulith.Infrastructure.Storage.Azure.Internal;

namespace Modulith.Infrastructure.Storage;

public static class Extension
{
    public static void AddAzureStorage(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AzureBlobOption>()
            .Bind(builder.Configuration.GetSection(nameof(AzureBlobOption)));

        var option = builder.Configuration.GetSection(nameof(AzureBlobOption)).Get<AzureBlobOption>();
        Guard.Against.Null(option);

        builder.Services.AddSingleton<IAzureStorage>(new AzureStorage(option));
    }
}