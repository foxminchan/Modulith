using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.UseCases.Categories.AddItem;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateCategoryRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/categories", async (CreateCategoryRequest payload) => await HandleAsync(payload))
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Category))
            .WithName("Create Category")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        AddItemCommand command = new(request.Name, request.Description);

        var result = await sender.Send(command, cancellationToken);

        CreateCategoryResponse response = new(result.Value);

        return Results.Created($"/api/v1/categories/{response.Id}", response);
    }
}