using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Baskets.UseCases.UpdateItem;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/baskets", async (UpdateBasketRequest request) => await HandleAsync(request))
            .Produces<UpdateBasketResponse>()
            .WithTags(nameof(Baskets))
            .WithName("Update Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        UpdateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        UpdateItemCommand command = new(request.CustomerId, request.Item);

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateBasketResponse
        {
            Basket = result.Value
        };

        return Results.Ok(response);
    }
}