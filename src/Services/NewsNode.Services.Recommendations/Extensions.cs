using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Recommendations.Recommendations;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Recommendations;

public static class Extensions
{
    public static IServiceCollection AddRecommendations(this IServiceCollection services)
    {
        services.AddSingleton<IRecommendationsService, RecommendationsService>();
        return services;
    }
}