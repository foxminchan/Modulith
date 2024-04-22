using FluentValidation;

namespace Modulith.Modules.Baskets.UseCases.GetBasket;

public sealed class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator() => RuleFor(x => x.BasketId).NotEmpty();
}