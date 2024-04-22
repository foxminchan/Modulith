using FluentValidation;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Baskets.UseCases.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Item.Id)
            .NotEmpty();

        RuleFor(x => x.Item.ProductName)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Item.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Item.Price)
            .GreaterThanOrEqualTo(0);
    }
}