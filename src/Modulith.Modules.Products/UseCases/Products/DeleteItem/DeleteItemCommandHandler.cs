using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.Modules.Products.Infrastructures.Storage.Azure;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.DeleteItem;

public sealed class DeleteItemCommandHandler(IRepository<Product> repository, IAzureStorage storage)
    : ICommandHandler<DeleteItemCommand, Result>
{
    public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        ProductByIdSpec spec = new(request.Id);
        var product = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (request.IsRemoveImage && product.Image is not null && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await storage.RemoveFileAsync(product.Image.ImageUrl, cancellationToken);

        product.Delete();
        await repository.UpdateAsync(product, cancellationToken);

        product.DeleteBasketItem(product.Id);
        return Result.Success();
    }
}