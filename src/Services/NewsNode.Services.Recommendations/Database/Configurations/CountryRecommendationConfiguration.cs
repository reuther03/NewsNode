using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Services.Recommendations.Database.Configurations;

public class CountryRecommendationConfiguration : IEntityTypeConfiguration<CountryRecommendation>
{
    public void Configure(EntityTypeBuilder<CountryRecommendation> builder)
    {
        builder.Property(x => x.Country)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Hashtag)
            .HasConversion(x => x.Value, x => new Hashtag(x))
            .IsRequired();
    }
}