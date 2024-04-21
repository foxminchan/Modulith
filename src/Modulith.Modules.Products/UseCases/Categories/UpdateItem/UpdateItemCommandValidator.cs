using FluentValidation;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Products.UseCases.Categories.UpdateItem;

public sealed class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Description)
            .MaximumLength(DatabaseSchemaLength.LONG_LENGTH);
    }
}