using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Modulith.Modules.Orders.Contracts;
using Modulith.Modules.Orders.Domain;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.UseCases.AddOrder;

public sealed class AddOrderCommandHandler(IRepository<Order> repository, ILogger<AddOrderCommandHandler> logger)
    : ICommandHandler<AddOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Factory.Create(
            request.CustomerId,
            request.Code,
            request.Items.Select(x => new OrderItem(x.Price, x.Quantity, x.Id))
        );

        logger.LogInformation("[{Command}] Order information: {Request}", nameof(AddOrderCommand),
            JsonSerializer.Serialize(request));

        await repository.AddAsync(order, cancellationToken);

        return Result<Guid>.Success(order.Id);
    }
}