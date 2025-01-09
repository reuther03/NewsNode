using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Recommendations.Database;

public class RecommendationsService : IRecommendationsService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RecommendationsService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async Task CreateActionRecommendation(UserId userId, List<Hashtag> hashtags, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        if (await context.ActionRecommendations.AnyAsync(x => x.UserId == userId && hashtags.Contains(x.Hashtag), cancellationToken))
            return;

        foreach (var recommendation in hashtags.Select(hashtag => ActionRecommendation.Create(userId, hashtag)))
            context.Recommendations.Add(recommendation);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task IncrementRecommendation(UserId userId, List<Hashtag> hashtags, PostActionType postActionType,
        CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.ActionRecommendations
            .Where(x => x.UserId == userId && hashtags.Contains(x.Hashtag))
            .ToListAsync(cancellationToken);

        if (recommendations.Count == 0)
            return;

        foreach (var recommendation in recommendations)
            recommendation.IncrementScore(postActionType);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Hashtag>> GetRecommendedHashtags(UserId userId, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.ActionRecommendations
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => (int)x.Weight)
            .ThenByDescending(x => x.Score)
            .Select(x => x.Hashtag)
            .ToListAsync(cancellationToken);

        return recommendations;
    }
}