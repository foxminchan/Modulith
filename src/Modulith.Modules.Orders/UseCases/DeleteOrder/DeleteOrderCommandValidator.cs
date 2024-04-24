using FluentValidation;

namespace Modulith.Modules.Orders.UseCases.DeleteOrder;

public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator() => RuleFor(x => x.OrderId).NotEmpty();
}