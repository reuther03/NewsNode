using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class PostActionConfiguration : IEntityTypeConfiguration<PostAction>
{
    public void Configure(EntityTypeBuilder<PostAction> builder)
    {
        builder.ToTable("Post_actions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserProfileId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.PostId)
            .HasConversion(x => x.Value, x => new PostId(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.ActionType)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne<UserProfile>()
            .WithMany(x => x.PostActions)
            .HasForeignKey(x => x.UserProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}