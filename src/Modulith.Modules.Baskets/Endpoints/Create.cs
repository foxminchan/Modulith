using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Baskets.UseCases.AddItem;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/baskets", async (
                CreateBasketRequest payload
            ) => await HandleAsync(payload))
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Baskets))
            .WithName("Create Basket")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        CreateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        AddItemCommand command = new(request.CustomerId, request.Item);

        var result = await sender.Send(command, cancellationToken);

        CreateBasketResponse response = new(result.Value);

        return Results.Created($"/api/v1/baskets/{response.Id}", response);
    }
}