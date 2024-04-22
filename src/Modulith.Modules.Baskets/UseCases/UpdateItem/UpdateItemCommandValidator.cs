using FluentValidation;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Baskets.UseCases.UpdateItem;

public sealed class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
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