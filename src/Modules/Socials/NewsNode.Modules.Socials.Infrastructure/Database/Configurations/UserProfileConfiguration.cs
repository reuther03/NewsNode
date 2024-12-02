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
        builder.ToTable("user_profiles", "socials");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.OwnsMany(x => x.FollowIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("UserProfileId");
            ownedBuilder.ToTable("user_followers");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("FollowerId");

            builder.Metadata
                .FindNavigation(nameof(UserProfile.FollowIds))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}