using System.ComponentModel.DataAnnotations;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Users.Contracts;

public sealed class Address(string street, string city, string province) : ValueObject, IValidatableObject
{
    public string Street { get; set; } = street;
    public string City { get; set; } = city;
    public string Province { get; set; } = province;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Street) || Street.Length > 50)
            yield return new("Street is required and must be less than 50 characters", [nameof(Street)]);

        if (string.IsNullOrWhiteSpace(City) || City.Length > 50)
            yield return new("City is required and must be less than 50 characters", [nameof(City)]);

        if (string.IsNullOrWhiteSpace(Province) || Province.Length > 50)
            yield return new("Province is required and must be less than 50 characters", [nameof(Province)]);
    }

    public override string ToString() => $"{Street}, {City}, {Province}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ToString();
    }
}