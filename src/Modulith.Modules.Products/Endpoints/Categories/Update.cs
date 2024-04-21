using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.UseCases.Categories.UpdateItem;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/categories", async (UpdateCategoryRequest request) => await HandleAsync(request))
            .Produces<UpdateCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName("Update Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        UpdateItemCommand command = new(request.Id, request.Name, request.Description);

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateCategoryResponse
        {
            Category = result.Value
        };

        return Results.Ok(response);
    }
}