﻿using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Infrastructures.Storage.Azure;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.AddItem;

public sealed class AddItemCommandHandler(
    IRepository<Product> repository,
    ILogger<AddItemCommandHandler> logger,
    IAzureStorage storage) : ICommandHandler<AddItemCommand, Result<Guid>>

{
    public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var result = string.Empty;

        if (request.Image is not null)
            result = await storage.UploadFileAsync(request.Image, cancellationToken);

        Product product = new(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice,
            string.IsNullOrWhiteSpace(result) ? null : new(result, request.Alt ?? request.Name, request.Name)
        );

        logger.LogInformation("[{Command}] Product information: {Product}", nameof(AddItemCommand),
            JsonSerializer.Serialize(product));

        await repository.AddAsync(product, cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}