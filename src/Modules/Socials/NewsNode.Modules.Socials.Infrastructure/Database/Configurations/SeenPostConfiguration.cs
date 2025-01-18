using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class SeenPostConfiguration : IEntityTypeConfiguration<SeenPost>
{
    public void Configure(EntityTypeBuilder<SeenPost> builder)
    {
        builder.ToTable("SeenPosts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.PostId)
            .HasConversion(x => x.Value, x => PostId.From(x))
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .IsRequired();

        builder.Property(x => x.SeenAt)
            .IsRequired();

        builder.HasOne<UserProfile>()
            .WithMany(x => x.SeenPosts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}