using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Baskets.UseCases.GetBasket;
using Modulith.Modules.Orders.Contracts;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class CheckOut(ISender sender) : IEndpoint<IResult, CheckOutBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/orders",
                async (CheckOutBasketRequest request) => await HandleAsync(new(request.BasketId, request.Code)))
            .Produces<CheckOutBasketResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Baskets))
            .WithName("Create Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(CheckOutBasketRequest request, CancellationToken cancellationToken = default)
    {
        GetBasketQuery basketQuery = new(request.BasketId);

        var requestBasket = await sender.Send(basketQuery, cancellationToken);

        var basket = requestBasket.Value;

        if (basket is null)
            return Results.NotFound("You don't have any basket.");

        var items = basket.Items.Select(x => new OrderItemCreateRequest(x.ProductId, x.Quantity, x.Price)).ToList();

        AddOrderCommand command = new(request.Code, basket.Id, items);

        var result = await sender.Send(command, cancellationToken);

        CheckOutBasketResponse response = new(result.Value);

        return Results.Created($"/api/v1/orders/{response.BasketId}", response);
    }
}