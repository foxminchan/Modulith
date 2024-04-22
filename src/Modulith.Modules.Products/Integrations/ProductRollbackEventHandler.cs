﻿using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Contracts;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.SharedKernel.Repositories;
using System.Text.Json;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Integrations;

public sealed class ProductRollbackEventHandler(
    IRepository<Product> repository,
    ILogger<ProductRollbackEventHandler> logger) : INotificationHandler<ProductRollbackEvent>
{
    public async Task Handle(ProductRollbackEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Request to add stock to products with IDs: {ProductIds}",
            nameof(ProductRollbackEvent), JsonSerializer.Serialize(notification.Items.Keys));

        foreach (var item in notification.Items)
        {
            ProductId id = new(item.Key);
            ProductByIdSpec spec = new(id);
            var product = await repository.GetByIdAsync(spec, cancellationToken);
            Guard.Against.NotFound(item.Key, product);
            product.AddStock(item.Value);
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}