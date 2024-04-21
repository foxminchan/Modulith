using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Specifications;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.GetItem;

public sealed class GetItemQueryHandler(IReadRepository<Category> repository)
    : IQueryHandler<GetItemQuery, Result<CategoryVm>>
{
    public async Task<Result<CategoryVm>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        CategoryByIdSpec spec = new(request.Id);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        CategoryVm response = new(entity.Id, entity.Name, entity.Description);
        return Result<CategoryVm>.Success(response);
    }
}