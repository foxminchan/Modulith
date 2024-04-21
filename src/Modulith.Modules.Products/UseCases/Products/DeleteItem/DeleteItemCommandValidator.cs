using FluentValidation;

namespace Modulith.Modules.Products.UseCases.Products.DeleteItem;

public sealed class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}