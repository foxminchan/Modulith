using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modulith.Modules.Users.Domain;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Users.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FullName)
            .HasMaxLength(DatabaseSchemaLength.SHORT_LENGTH)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(DatabaseSchemaLength.TINY_LENGTH);

        builder.OwnsOne(
            u => u.Address,
            a => a.ToJson()
        );
    }
}