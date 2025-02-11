using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsNode.Services.GroupChats.GroupChats;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Services.GroupChats.Database.Configurations;

public class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.ToTable("group_chats");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.OwnsMany(x => x.Hashtags)
            .Property(x => x.Value)
            .HasColumnName("Hashtag")
            .IsRequired();

        builder.OwnsMany(x => x.Participants, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("GroupId");
            ownedBuilder.ToTable("GroupChatParticipants");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("UserId");

            builder.Metadata
                .FindNavigation(nameof(GroupChat.Participants))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}