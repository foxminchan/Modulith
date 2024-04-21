using Ardalis.Result;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.DeleteItem;

public sealed record DeleteItemCommand(ProductId Id, bool IsRemoveImage) : ICommand<Result>;