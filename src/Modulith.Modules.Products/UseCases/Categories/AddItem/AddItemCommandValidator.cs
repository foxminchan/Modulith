using FluentValidation;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Products.UseCases.Categories.AddItem;

public sealed class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Description)
            .MaximumLength(DatabaseSchemaLength.LONG_LENGTH);
    }
}