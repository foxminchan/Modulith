using Microsoft.Extensions.Hosting;
using Modulith.Modules.Products.Data;

namespace Modulith.Modules.Products;

public static class Extension
{
    public static void AddProductModule(this IHostApplicationBuilder builder)
    {
        builder.Services.AddProductDbContext(builder.Configuration);
    }
}