using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modulith.Infrastructure.Endpoint;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.UseCases.Products.UpdateItem;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/products", async (
                    [FromForm] Guid id,
                    [FromForm] string name,
                    [FromForm] string? productCode,
                    [FromForm] string? detail,
                    [FromForm] int quantity,
                    [FromForm] Guid? categoryId,
                    [FromForm] decimal price,
                    [FromForm] decimal priceSale,
                    [FromForm] bool isDeleteImage,
                    [FromForm] IFormFile? image,
                    [FromForm] string? alt)
                => await HandleAsync(new(
                    id, name, productCode, detail, quantity, categoryId, price, priceSale, isDeleteImage, image, alt
                )))
            .Produces<UpdateProductResponse>()
            .WithTags(nameof(Product))
            .WithName("Update Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        UpdateItemCommand command = new(
            request.Id,
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            new(request.Price, request.PriceSale),
            request.IsDeleteImage,
            request.Image,
            request.Alt
        );

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateProductResponse
        {
            Product = result.Value
        };

        return Results.Ok(response);
    }
}