﻿using FluentValidation;

namespace Modulith.Modules.Products.UseCases.Categories.GetItem;

public sealed class GetItemQueryValidator : AbstractValidator<GetItemQuery>
{
    public GetItemQueryValidator() => RuleFor(x => x.Id).NotEmpty();
}