using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.DeleteItem;

public sealed record DeleteItemCommand(CategoryId Id) : ICommand<Result>;