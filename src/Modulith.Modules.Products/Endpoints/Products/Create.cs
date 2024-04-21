using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.UseCases.Products.AddItem;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/products", async (
                [FromForm] string name,
                [FromForm] string? productCode,
                [FromForm] string? detail,
                [FromForm] int quantity,
                [FromForm] CategoryId? categoryId,
                [FromForm] decimal price,
                [FromForm] decimal priceSale,
                [FromForm] IFormFile? image,
                [FromForm] string? alt
            ) => await HandleAsync(new(
                name, productCode, detail, quantity, categoryId, price, priceSale, image, alt)
            ))
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Product))
            .WithName("Create Product")
            .MapToApiVersion(new(1, 0))
            .DisableAntiforgery()
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        AddItemCommand command = new(
            request.ProductName,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            new(request.Price, request.PriceSale),
            request.Image,
            request.Alt
        );

        var result = await sender.Send(command, cancellationToken);

        CreateProductResponse response = new(result.Value);

        return Results.Created($"/api/v1/products/{response.Id}", response);
    }
}