using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => PostId.From(x))
            .ValueGeneratedNever();

        builder.Property(x => x.Content)
            .HasMaxLength(250);

        builder.Property(x => x.PostedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.OwnsMany(x => x.Hashtags)
            .Property(x => x.Value)
            .HasColumnName("Hashtag")
            .HasConversion(x => x, x => new Hashtag(x));

        builder.Property(x => x.Likes);
        builder.Property(x => x.Bookmarks);
        builder.Property(x => x.Reposts);

        builder.HasMany(x => x.Comments)
            .WithOne()
            .HasForeignKey("PostId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PostImgs)
            .WithOne()
            .HasForeignKey("PostId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}