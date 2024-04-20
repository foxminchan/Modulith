using System.ComponentModel.DataAnnotations;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;

public sealed class ProductPrice(decimal price = 0, decimal priceSale = 0) : ValueObject, IValidatableObject
{
    public decimal Price { get; set; } = price;
    public decimal PriceSale { get; set; } = priceSale;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Price < 0)
            yield return new("Price must be greater than or equal to 0", [nameof(Price)]);

        if (PriceSale < 0)
            yield return new("PriceSale must be greater than or equal to 0", [nameof(PriceSale)]);

        if (PriceSale > Price)
            yield return new("PriceSale must be less than or equal to Price", [nameof(PriceSale)]);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PriceSale;
    }
}