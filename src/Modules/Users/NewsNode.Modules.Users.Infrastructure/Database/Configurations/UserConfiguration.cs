using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Users.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever();

        builder.Property(x => x.Username)
            .HasConversion(x => x.Value, x => new Name(x))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired();

        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(x => x.Country)
                .HasColumnName("Country")
                .IsRequired();

            location.Property(x => x.City)
                .HasColumnName("City")
                .IsRequired();
        });


        builder.HasIndex(x => x.Email).IsUnique();
    }
}