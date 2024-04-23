using Modulith.Modules.Products.Domain.CategoryAggregate;

namespace Modulith.Modules.Products.ViewModels;

public sealed record CategoryVm(
    Guid Id,
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