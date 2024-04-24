using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Modules.Orders.Domain;
using Modulith.Modules.Orders.Domain.Specifications;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.UseCases.DeleteOrder;

public sealed class DeleteOrderCommandHandler(IRepository<Order> repository)
    : ICommandHandler<DeleteOrderCommand, Result>
{
    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        OrderByIdSpec spec = new(request.OrderId);
        var order = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.OrderId, order);
        await repository.DeleteAsync(order, cancellationToken);
        return Result.Success();
    }
}