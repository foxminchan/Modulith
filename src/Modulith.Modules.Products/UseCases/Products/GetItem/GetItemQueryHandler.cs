using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.GetItem;

public sealed class GetItemQueryHandler(IReadRepository<Product> repository)
    : IQueryHandler<GetItemQuery, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        ProductByIdSpec spec = new(request.Id);
        var product = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, product);
        var response = ProductVm.FromEntity(product);
        return Result<ProductVm>.Success(response);
    }
}