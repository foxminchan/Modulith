using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.Modules.Products.Infrastructures.Storage.Azure;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.UpdateItem;

public sealed class UpdateItemCommandHandler(
    IRepository<Product> repository,
    ILogger<UpdateItemCommandHandler> logger,
    IAzureStorage storage) : ICommandHandler<UpdateItemCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(new ProductByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (request.IsDeleteImage || request.Image is not null)
            await RemoveObsoleteImagesAsync(product);

        var result = string.Empty;

        if (request.Image is not null)
            result = await storage.UploadFileAsync(request.Image, cancellationToken);

        product.Update(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice,
            string.IsNullOrWhiteSpace(result) ? null : new(result, request.Alt ?? request.Name, request.Name)
        );

        logger.LogInformation("[{Command}] Product information: {Product}", nameof(UpdateItemCommand),
            JsonSerializer.Serialize(product));

        await repository.UpdateAsync(product, cancellationToken);

        var response = ProductVm.FromEntity(product);

        return Result<ProductVm>.Success(response);
    }

    private async Task RemoveObsoleteImagesAsync(Product product)
    {
        if (product.Image is not null && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await storage.RemoveFileAsync(product.Image.ImageUrl);
    }
}