using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Recommendations.Database;
using NewsNode.Services.Recommendations.Jobs;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Services.Recommendations;

public static class Extensions
{
    public static IServiceCollection AddRecommendations(this IServiceCollection services)
    {
        services.AddPostgres<RecommendationsDbContext>();
        services.AddScoped<RecommendationsDbContext>();
        services.AddSingleton<IRecommendationsService, RecommendationsService>();
        services.AddHostedService<RecommendationJob>();
        return services;
    }
}