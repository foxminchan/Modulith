using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.UseCases.Products.ListItems;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class List(ISender sender) : IEndpoint<IResult, ListProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products", async (
                string? search,
                string? orderBy,
                bool isAscending = true,
                int pageIndex = 1,
                int pageSize = 20) => await HandleAsync(new(pageIndex, pageSize, search, orderBy, isAscending)
            ))
            .Produces<ListProductResponse>()
            .WithTags(nameof(Product))
            .WithName("List Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        ListProductRequest request,
        CancellationToken cancellationToken = default)
    {
        ListItemsQuery query = new(
            request.Search,
            request.IsAscending,
            request.OrderBy,
            request.PageIndex,
            request.PageSize);

        var result = await sender.Send(query, cancellationToken);

        var response = new ListProductResponse
        {
            PagedInfo = result.PagedInfo,
            Products = result.Value
        };

        return Results.Ok(response);
    }
}