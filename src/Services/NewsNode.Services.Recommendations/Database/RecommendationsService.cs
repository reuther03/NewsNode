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

    public async Task IncrementActionRecommendation(UserId userId, List<Hashtag> hashtags, PostActionType postActionType,
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

    public async Task<Dictionary<Hashtag, RecommendationWeight>> GetRecommendedHashtags(UserId userId, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.ActionRecommendations
            .Where(x => x.UserId == userId && x.Weight >= RecommendationWeight.MediumLow)
            .OrderByDescending(x => x.Weight)
            .ThenByDescending(x => x.Score)
            .Take(5)
            .Select(x => new { x.Hashtag, x.Weight })
            .ToListAsync(cancellationToken);

        return recommendations.ToDictionary(x => x.Hashtag, x => x.Weight);
    }

    public async Task<Dictionary<Hashtag, RecommendationWeight>> GetLessInterestedHashtags(UserId userId, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.ActionRecommendations
            .Where(x => x.UserId == userId && x.Weight < RecommendationWeight.MediumLow)
            .OrderByDescending(x => x.Weight)
            .ThenByDescending(x => x.Score)
            .Take(5)
            .Select(x => new { x.Hashtag, x.Weight })
            .ToListAsync(cancellationToken);

        return recommendations.ToDictionary(x => x.Hashtag, x => x.Weight);
    }

    public async Task<List<UserId>> GetRecommendedProfiles(UserId userId, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var userTopHashtags = await context.ActionRecommendations
            .Where(x => x.UserId == userId && x.Weight >= RecommendationWeight.MediumLow)
            .OrderByDescending(x => x.Weight)
            .Take(5)
            .Select(x => x.Hashtag)
            .ToListAsync(cancellationToken);

        // pomyslec czy da sie to w ogole zrobic zeby zwrocilo hashtagi z postami

        var similarUsers = await context.ActionRecommendations
            .Where(x => userTopHashtags.Contains(x.Hashtag) && x.UserId != userId)
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken);

        return similarUsers;
    }

    public async Task CreateCountryRecommendation(string country, List<Hashtag> hashtags, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        if (await context.CountryRecommendations.AnyAsync(x => x.Country == country && hashtags.Contains(x.Hashtag), cancellationToken))
            return;

        foreach (var recommendation in hashtags.Select(hashtag => CountryRecommendation.Create(country, hashtag)))
            context.Recommendations.Add(recommendation);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task IncrementCountryRecommendation(string country, List<Hashtag> hashtags, PostActionType postActionType,
        CancellationToken cancellationToken = default)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        var recommendations = await context.CountryRecommendations
            .Where(x => x.Country == country && hashtags.Contains(x.Hashtag))
            .ToListAsync(cancellationToken);

        if (recommendations.Count == 0)
            return;

        foreach (var recommendation in recommendations)
            recommendation.IncrementScore(postActionType);

        await context.SaveChangesAsync(cancellationToken);
    }
}