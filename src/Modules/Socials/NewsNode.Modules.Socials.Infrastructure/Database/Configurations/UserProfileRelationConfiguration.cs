using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Configurations;

public class UserProfileRelationConfiguration : IEntityTypeConfiguration<UserProfileRelation>
{
    public void Configure(EntityTypeBuilder<UserProfileRelation> builder)
    {
        builder.ToTable("User_profile_relations");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.TargetUserProfileId)
            .HasConversion(x => x.Value, x => new UserId(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.RelationStatus)
            .HasConversion<string>()
            .IsRequired();
    }
}