using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Shared.Abstractions.Services;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}