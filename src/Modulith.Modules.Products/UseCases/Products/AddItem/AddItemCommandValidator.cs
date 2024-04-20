using FluentValidation;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Products.UseCases.Products.AddItem;

public sealed class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseSchemaLength.SMALL_LENGTH);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseSchemaLength.MAX_LENGTH);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice.PriceSale)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(x => x.ProductPrice.Price);

        RuleFor(x => x.Alt)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

    }
}