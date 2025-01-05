using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsNode.Services.Recommendations.Database;

namespace NewsNode.Services.Recommendations.Recommendations;

public class DecayScoreJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DecayScoreJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DecayScore(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task DecayScore(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.Recommendations.ToListAsync(cancellationToken);

        foreach (var recommendation in recommendations)
        {
            var daysSinceInteraction = (DateTime.UtcNow - recommendation.LastInteraction).Days;

            if (daysSinceInteraction < 3)
                continue;

            var score = recommendation.Score - 1;

            recommendation.SetScore(score);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}