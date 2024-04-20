using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Infrastructure.Storage.Azure;
using Modulith.Infrastructure.Storage.Azure.Internal;
using Modulith.Infrastructure.Validator;

namespace Modulith.Infrastructure.Storage;

public static class Extension
{
    public static void AddAzureStorage(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AzureBlobOption>()
            .Bind(builder.Configuration.GetSection(nameof(AzureBlobOption)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        builder.Services.AddSingleton<IAzureStorage>(
            new AzureStorage(builder.Services.BuildServiceProvider().GetRequiredService<AzureBlobOption>())
        );
    }
}