using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.UseCases.Categories.GetItem;
using Modulith.Modules.Products.ViewModels;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class Get(ISender sender) : IEndpoint<IResult, GetCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces<CategoryVm>()
            .WithTags(nameof(Category))
            .WithName("Get Category By Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        GetCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        GetItemQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result.Value);
    }
}