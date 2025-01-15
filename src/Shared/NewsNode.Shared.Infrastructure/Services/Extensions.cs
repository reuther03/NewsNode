using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Shared.Infrastructure.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRedisCacheService, RedisCacheService>();

        return services;
    }
}