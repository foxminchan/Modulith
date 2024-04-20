using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;

public sealed class ProductImage(string? imageUrl, string? alt, string? title) : ValueObject, IValidatableObject
{
    public string? ImageUrl { get; set; } = Guard.Against.NullOrEmpty(imageUrl);
    public string? Alt { get; set; } = alt;
    public string? Title { get; set; } = title;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ImageUrl) || ImageUrl.Length > 255)
            yield return new("ImageUrl is required and must be less than 255 characters", [nameof(ImageUrl)]);

        if (!string.IsNullOrWhiteSpace(Alt) && Alt.Length > 255)
            yield return new("Alt must be less than 255 characters", [nameof(Alt)]);

        if (!string.IsNullOrWhiteSpace(Title) && Title.Length > 255)
            yield return new("Title must be less than 255 characters", [nameof(Title)]);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ImageUrl ?? string.Empty;
        yield return Alt ?? string.Empty;
        yield return Title ?? string.Empty;
    }
}