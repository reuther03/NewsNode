using Microsoft.EntityFrameworkCore;
using NewsNode.Services.Recommendations.Recommendations;

namespace NewsNode.Services.Recommendations.Database;

public class RecommendationsDbContext : DbContext
{
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("recommendations");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}