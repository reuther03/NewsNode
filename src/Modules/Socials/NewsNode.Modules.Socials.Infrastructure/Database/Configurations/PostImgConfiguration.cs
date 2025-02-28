using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.Post;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class PostImgConfiguration : IEntityTypeConfiguration<ContentImg>
{
    public void Configure(EntityTypeBuilder<ContentImg> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FileUrl)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired();
    }
}