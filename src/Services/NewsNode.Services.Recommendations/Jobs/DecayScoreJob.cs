using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsNode.Services.Recommendations.Database;

namespace NewsNode.Services.Recommendations.Jobs;

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
            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }

    private async Task DecayScore(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        const int batchSize = 1000;
        var totalRecommendations = await context.Recommendations.CountAsync(cancellationToken);
        for (var i = 0; i < totalRecommendations; i += batchSize)
        {
            var recommendations = await context.Recommendations.Skip(i).Take(batchSize).ToListAsync(cancellationToken);

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
}