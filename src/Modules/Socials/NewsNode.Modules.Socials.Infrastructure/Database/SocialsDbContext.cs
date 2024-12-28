using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Modules.Socials.Domain.UserProfile;

namespace NewsNode.Modules.Socials.Infrastructure.Database;

internal class SocialsDbContext : DbContext, ISocialsDbContext
{
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<UserProfileFollow> UserProfileFollowers => Set<UserProfileFollow>();
    public DbSet<UserProfileStatus> UserProfileStatuses => Set<UserProfileStatus>();
    public DbSet<PostAction> PostActions => Set<PostAction>();
    public DbSet<Post> Posts => Set<Post>();

    public SocialsDbContext(DbContextOptions<SocialsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("socials");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}