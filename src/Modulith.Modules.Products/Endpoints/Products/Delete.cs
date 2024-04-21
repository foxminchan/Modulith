using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.UseCases.Products.DeleteItem;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/products/{id}",
                async (ProductId id, bool isRemoveImage = false) => await HandleAsync(new(id, isRemoveImage)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Product))
            .WithName("Delete Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken = default)
    {
        DeleteItemCommand command = new(request.Id, request.IsRemoveImage);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}