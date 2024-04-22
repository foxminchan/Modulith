using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Baskets.UseCases.DeleteItem;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/baskets/{id:guid}", async (Guid id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Baskets))
            .WithName("Delete Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        DeleteBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteItemCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}