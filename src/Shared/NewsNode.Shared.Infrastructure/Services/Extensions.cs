using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Services.CloudinaryService;

namespace NewsNode.Shared.Infrastructure.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        services.AddCloudinary(configuration);

        return services;
    }
}