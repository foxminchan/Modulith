using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.AddItem;

public sealed record AddItemCommand(
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    IFormFile? Image,
    string? Alt) : ICommand<Result<ProductId>>;