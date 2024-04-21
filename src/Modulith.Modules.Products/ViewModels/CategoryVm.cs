using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.ViewModels;

public sealed record CategoryVm(
    CategoryId Id,
    string? Name,
    string? Description
);