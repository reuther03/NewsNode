

// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Modules;

namespace NewsNode.Services.Recommendations;

public class RecommendationsModule : IModule
{
    public const string BasePath = "recommendations-module";

    public string Name => "Recommendations";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        services.AddRecommendations();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}