using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.UseCases.Categories.ListItems;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class List(ISender sender) : IEndpointWithoutRequest<IResult>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories", HandleAsync)
            .Produces<ListCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("List Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        ListItemsQuery query = new();

        var result = await sender.Send(query, cancellationToken);

        var response = new ListCategoryResponse
        {
            Categories = result.Value
        };

        return Results.Ok(response);
    }
}