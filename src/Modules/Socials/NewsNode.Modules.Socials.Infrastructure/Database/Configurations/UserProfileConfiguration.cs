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

        builder.OwnsMany(x => x.RepostedPosts, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("UserProfileId");
            ownedBuilder.ToTable("User_profile_reposted_posts");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("PostId");

            builder.Metadata
                .FindNavigation(nameof(UserProfile.RepostedPosts))
                !.SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.HasIndex(x => x.Email).IsUnique();
    }
}