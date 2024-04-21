using FluentValidation;

namespace Modulith.Modules.Products.UseCases.Products.GetItem;

public sealed class GetItemQueryValidator : AbstractValidator<GetItemQuery>
{
    public GetItemQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}