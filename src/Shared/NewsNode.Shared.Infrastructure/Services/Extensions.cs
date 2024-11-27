using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Shared.Infrastructure.Services;

internal static class Extensions
{
    private const string CorsPolicy = "cors";

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}