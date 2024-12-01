using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Modules.Socials.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}