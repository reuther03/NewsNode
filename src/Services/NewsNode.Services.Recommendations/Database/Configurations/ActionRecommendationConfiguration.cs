using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Database.Configurations;

public class ActionRecommendationConfiguration : IEntityTypeConfiguration<ActionRecommendation>
{
    public void Configure(EntityTypeBuilder<ActionRecommendation> builder)
    {
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .IsRequired();
    }
}