using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Contracts;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.SharedKernel.Repositories;

namespace Modulith.Modules.Products.Integrations;

public sealed class ProductUpdateQuantityEventHandler(
    IRepository<Product> repository,
    ILogger<ProductUpdateQuantityEventHandler> logger) : INotificationHandler<ProductUpdateQuantityEvent>
{
    public async Task Handle(ProductUpdateQuantityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Request to remove stock from product with ID: {ProductId}",
            nameof(ProductUpdateQuantityEvent), notification.ProductId);
        ProductByIdSpec spec = new(notification.ProductId);
        var product = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(notification.ProductId, product);
        if (notification.IsReduce)
            product.AddStock(notification.Quantity);
        else
            product.RemoveStock(notification.Quantity);
        await repository.UpdateAsync(product, cancellationToken);
    }
}