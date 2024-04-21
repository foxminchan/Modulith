using FluentValidation;

namespace Modulith.Modules.Products.UseCases.Categories.DeleteItem;

public sealed class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}