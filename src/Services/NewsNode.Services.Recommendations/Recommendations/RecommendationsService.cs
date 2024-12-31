using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Recommendations.Recommendations;

public class RecommendationsService : IRecommendationsService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RecommendationsService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
}