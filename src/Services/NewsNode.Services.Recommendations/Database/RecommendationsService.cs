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


    public async Task CreateRecommendation(UserId userId, List<Hashtag> hashtags, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        if (context.Recommendations.Any(x => x.UserId == userId && hashtags.Any(z => z.Value == x.Hashtag)))
            return;

        foreach (var recommendation in hashtags.Select(hashtag => Recommendation.Create(userId, hashtag)))
            context.Recommendations.Add(recommendation);

        await context.SaveChangesAsync(cancellationToken);
    }

    public Task IncrementRecommendation(UserId userId, Hashtag hashtag, PostActionType postActionType, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendation = context.Recommendations.FirstOrDefault(x => x.UserId == userId && x.Hashtag == hashtag.Value);

        if (recommendation is null)
            return Task.CompletedTask;


        recommendation.IncrementScore(postActionType);

        return context.SaveChangesAsync(cancellationToken);
    }
}