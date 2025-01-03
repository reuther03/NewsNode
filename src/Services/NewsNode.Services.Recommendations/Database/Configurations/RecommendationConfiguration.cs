using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Database.Configurations;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.ToTable("Recommendations");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.Hashtag)
            .HasConversion(x => x.Value, x => new Hashtag(x))
            .IsRequired();

        builder.Property(x => x.Score)
            .IsRequired();

        builder.Property(x => x.Weight)
            .IsRequired();
    }
}