using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.Recommendations.Recommendations;

namespace NewsNode.Services.Recommendations.Database.Configurations;

public class CountryRecommendationConfiguration : IEntityTypeConfiguration<CountryRecommendation>
{
    public void Configure(EntityTypeBuilder<CountryRecommendation> builder)
    {
        builder.Property(x => x.Country)
            .HasMaxLength(200)
            .IsRequired();
    }
}