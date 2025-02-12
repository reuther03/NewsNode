using Microsoft.EntityFrameworkCore;
using NewsNode.Services.GroupChats.GroupChats;

namespace NewsNode.Services.GroupChats.Database;

public class GroupChatsDbContext : DbContext
{
    public DbSet<GroupChat> GroupChats => Set<GroupChat>();

    public GroupChatsDbContext(DbContextOptions<GroupChatsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("group_chats");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}