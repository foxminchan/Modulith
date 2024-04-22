using FluentValidation;

namespace Modulith.Modules.Products.UseCases.Products.ListItems;

public sealed class ListItemsQueryValidator : AbstractValidator<ListItemsQuery>
{
    public ListItemsQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);
    }
}