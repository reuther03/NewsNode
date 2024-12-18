using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class UserProfileStatusConfiguration : IEntityTypeConfiguration<UserProfileStatus>
{
    public void Configure(EntityTypeBuilder<UserProfileStatus> builder)
    {
        builder.ToTable("User_profile_statuses");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.TargetUserId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne<UserProfile>()
            .WithMany(x => x.ProfileStatus)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}