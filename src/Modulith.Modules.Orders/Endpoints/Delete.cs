using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Orders.Domain;
using Modulith.Modules.Orders.UseCases.DeleteOrder;

namespace Modulith.Modules.Orders.Endpoints;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("orders/{id:guid}", async (Guid id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Order))
            .WithName("Delete Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        DeleteOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteOrderCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}