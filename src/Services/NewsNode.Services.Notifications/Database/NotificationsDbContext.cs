using Microsoft.EntityFrameworkCore;

namespace NewsNode.Services.Notifications.Database;

internal class NotificationsDbContext : DbContext
{


    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notifications");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}