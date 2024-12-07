using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Article;
using NewsNode.Modules.Socials.Domain.UserProfile;

namespace NewsNode.Modules.Socials.Infrastructure.Database;

internal class SocialsDbContext : DbContext, ISocialsDbContext
{
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
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