using FluentValidation;

namespace Modulith.Modules.Orders.UseCases.GetOrder;

public sealed class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}