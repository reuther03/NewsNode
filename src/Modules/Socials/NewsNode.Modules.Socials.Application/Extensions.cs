using Microsoft.Extensions.DependencyInjection;
using NewsNode.Modules.Socials.Application.Abstractions.Services;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddSingleton<ISocialService, SocialService>();
        services.AddScoped<ISeenPostService, SeenPostService>();
        return services;
    }
}