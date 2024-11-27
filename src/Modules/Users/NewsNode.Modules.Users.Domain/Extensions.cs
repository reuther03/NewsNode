using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Modules.Users.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}