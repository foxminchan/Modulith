using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Specifications;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.DeleteItem;

public sealed class DeleteItemCommandHandler(IRepository<Category> repository)
    : ICommandHandler<DeleteItemCommand, Result>
{
    public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        CategoryByIdSpec spec = new(request.Id);
        var category = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        await repository.DeleteAsync(category, cancellationToken);
        return Result.Success();
    }
}