using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modulith.Modules.Orders.Domain;
using Modulith.Persistence.Configurations;

namespace Modulith.Modules.Orders.Infrastructures.Data.Configurations;

public sealed class OrderItemConfiguration : BaseConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => new { e.OrderId, e.ProductId });

        builder.Property(e => e.OrderId)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ProductId)
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.Price)
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}