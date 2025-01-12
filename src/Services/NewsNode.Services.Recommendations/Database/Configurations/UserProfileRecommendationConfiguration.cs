using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Database.Configurations;

public class UserProfileRecommendationConfiguration : IEntityTypeConfiguration<UserProfileRecommendation>
{
    public void Configure(EntityTypeBuilder<UserProfileRecommendation> builder)
    {
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .IsRequired();

        builder.Property(x => x.TargetUserId)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .IsRequired();
    }
}