using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Persistence.Configurations;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Products.Infrastructures.Data.Configurations;

public sealed class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
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

        builder.Property(p => p.Name)
            .HasMaxLength(DatabaseSchemaLength.DEFAULT_LENGTH)
            .IsRequired();

        builder.OwnsOne(
            p => p.Price,
            e => e.ToJson()
        );

        builder.OwnsOne(
            p => p.Image,
            e => e.ToJson()
        );

        builder.Property(p => p.Quantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(p => p.ProductCode)
            .HasMaxLength(DatabaseSchemaLength.SMALL_LENGTH);

        builder.Property(p => p.Description)
            .HasMaxLength(DatabaseSchemaLength.MAX_LENGTH);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}