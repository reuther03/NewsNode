using Microsoft.EntityFrameworkCore;

namespace NewsNode.Services.Recommendations.Database;

public class RecommendationsDbContext : DbContext
{
    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("recommendations");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}