using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Orders.Domain;
using Modulith.Modules.Orders.UseCases.GetOrder;
using Modulith.Modules.Orders.ViewModels;

namespace Modulith.Modules.Orders.Endpoints;

public sealed class Get(ISender sender) : IEndpoint<IResult, GetOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/{id:guid}", async (Guid id) => await HandleAsync(new(id)))
            .Produces<OrderVm>()
            .WithTags(nameof(Order))
            .WithName("Get Order by Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken = default)
    {
        GetOrderQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}