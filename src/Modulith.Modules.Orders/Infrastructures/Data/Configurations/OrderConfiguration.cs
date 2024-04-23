using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modulith.Modules.Orders.Domain;
using Modulith.Persistence.Configurations;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Orders.Infrastructures.Data.Configurations;

public sealed class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasDefaultValueSql(UniqueId.UUID_ALGORITHM)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Code)
            .HasMaxLength(DatabaseSchemaLength.SMALL_LENGTH);
    }
}