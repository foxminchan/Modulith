using Ardalis.Result;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.GetItem;

public sealed record GetItemQuery(ProductId Id) : IQuery<Result<ProductVm>>;