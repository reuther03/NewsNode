using Microsoft.EntityFrameworkCore;
using NewsNode.Services.GroupChats.GroupChats;

namespace NewsNode.Services.GroupChats.Database;

internal class GroupChatDbContext : DbContext
{
    public DbSet<GroupChat> GroupChats => Set<GroupChat>();

    public GroupChatDbContext(DbContextOptions<GroupChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Group_Chats");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}