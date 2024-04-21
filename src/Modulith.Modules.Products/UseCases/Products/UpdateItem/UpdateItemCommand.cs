using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.UpdateItem;

public sealed record UpdateItemCommand(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    bool IsDeleteImage,
    IFormFile? Image,
    string? Alt) : ICommand<Result<ProductVm>>;