using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.UseCases.Categories.DeleteItem;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/categories/{id}", async (CategoryId id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Category))
            .WithName("Delete Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken = default)
    {
        DeleteItemCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}