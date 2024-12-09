using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class UserProfileFollowerConfiguration : IEntityTypeConfiguration<UserProfileFollower>
{
    public void Configure(EntityTypeBuilder<UserProfileFollower> builder)
    {
        builder.ToTable("User_profile_followers");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FollowerId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedNever()
            .IsRequired();
    }
}