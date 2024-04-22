using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Baskets.UseCases.GetBasket;
using Modulith.Modules.Baskets.ViewModels;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class Get(ISender sender) : IEndpoint<IResult, GetBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/baskets/{id}", async (Guid id) => await HandleAsync(new(id)))
            .Produces<CustomerBasketVm>()
            .WithTags(nameof(Baskets))
            .WithName("Get Basket By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        GetBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        GetBasketQuery query = new(request.BasketId);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}