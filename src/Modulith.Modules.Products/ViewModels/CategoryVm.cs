using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.ViewModels;

public sealed record CategoryVm(
    CategoryId Id,
    string? Name,
    string? Description
)
{
    public static CategoryVm FromEntity(Category category) =>
        new(
            category.Id,
            category.Name,
            category.Description
        );
}