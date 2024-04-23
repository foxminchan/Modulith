using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.UseCases.Products.GetItem;
using Modulith.Modules.Products.ViewModels;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class Get(ISender sender) : IEndpoint<IResult, GetProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/{id:guid}", async (Guid id) => await HandleAsync(new(id)))
            .Produces<ProductVm>()
            .WithTags(nameof(Product))
            .WithName("Get Product By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        GetProductRequest request,
        CancellationToken cancellationToken = default)
    {
        GetItemQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}