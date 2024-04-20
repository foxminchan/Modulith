using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Persistence.Configurations;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Products.Data.Configurations;

public sealed class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .HasDefaultValueSql(UniqueId.UUID_ALGORITHM)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(DatabaseSchemaLength.DEFAULT_LENGTH)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(DatabaseSchemaLength.LONG_LENGTH);

        builder.HasData(GetSampleCategoryData());
    }

    private static IEnumerable<Category> GetSampleCategoryData()
    {
        yield return new()
        {
            Name = "Fantasy",
            Description = "Books featuring magical or supernatural elements often set in imaginary worlds."
        };

        yield return new()
        {
            Name = "Science Fiction",
            Description = "Books exploring speculative concepts such as advanced science and technology, space exploration, or futuristic societies."
        };

        yield return new()
        {
            Name = "Mystery",
            Description = "Books centered around solving a crime or unraveling a puzzle, often featuring detectives or amateur sleuths."
        };

        yield return new()
        {
            Name = "Romance",
            Description = "Books focusing on romantic relationships and emotional connections between characters."
        };

        yield return new()
        {
            Name = "Historical Fiction",
            Description = "Books set in the past, often blending fictional characters and events with real historical contexts."
        };

        yield return new()
        {
            Name = "Thriller",
            Description = "Books designed to provoke excitement, tension, and suspense, often involving danger and high stakes."
        };

        yield return new()
        {
            Name = "Horror",
            Description = "Books intended to evoke fear, dread, or terror through supernatural or psychological elements."
        };

        yield return new()
        {
            Name = "Biography",
            Description = "Books recounting the life and experiences of a real person, often written by another individual."
        };

        yield return new()
        {
            Name = "Self-Help",
            Description = "Books offering advice, guidance, and strategies for personal growth, improvement, and self-discovery."
        };

        yield return new()
        {
            Name = "Cooking",
            Description = "Books providing recipes, cooking techniques, and culinary inspiration for preparing various dishes and cuisines."
        };
    }
}