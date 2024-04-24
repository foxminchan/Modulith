using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Orders.Domain;
using Modulith.Modules.Orders.Domain.Specifications;
using Modulith.Modules.Orders.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.UseCases.GetOrder;

public sealed class GetOrderQueryHandler(IReadRepository<Order> repository)
    : IQueryHandler<GetOrderQuery, Result<OrderVm>>
{
    public async Task<Result<OrderVm>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        OrderByIdSpec spec = new(request.Id);
        var order = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        var response = OrderVm.FromEntity(order);
        return Result<OrderVm>.Success(response);
    }
}