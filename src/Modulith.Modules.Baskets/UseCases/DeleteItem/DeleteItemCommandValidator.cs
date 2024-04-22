using FluentValidation;

namespace Modulith.Modules.Baskets.UseCases.DeleteItem;

public sealed class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}