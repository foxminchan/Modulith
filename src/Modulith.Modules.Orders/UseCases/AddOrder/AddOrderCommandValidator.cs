using FluentValidation;
using Modulith.Modules.Orders.Contracts;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Orders.UseCases.AddOrder;

public sealed class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator(IValidator<OrderItemCreateRequest> orderItemValidator)
    {
        RuleFor(x => x.Code)
            .MaximumLength(DatabaseSchemaLength.SMALL_LENGTH);

        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Items)
            .NotEmpty()
            .ForEach(x => x.SetValidator(orderItemValidator));
    }
}

public sealed class OrderItemCreateRequestValidator : AbstractValidator<OrderItemCreateRequest>
{
    public OrderItemCreateRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}