using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("User_profiles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.ProfileFollowers)
            .WithOne()
            .HasForeignKey("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProfileRelations)
            .WithOne()
            .HasForeignKey("UserProfileId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}