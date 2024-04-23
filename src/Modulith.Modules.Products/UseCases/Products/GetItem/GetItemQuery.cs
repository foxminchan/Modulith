using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.GetItem;

public sealed record GetItemQuery(Guid Id) : IQuery<Result<ProductVm>>;