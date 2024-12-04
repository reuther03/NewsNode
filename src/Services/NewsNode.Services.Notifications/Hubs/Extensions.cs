using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace NewsNode.Services.Notifications;

public static class Extensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSignalR();
        // services.AddHostedService<ServerTimeNotifier>();
        return services;
    }

    public static IEndpointRouteBuilder MapHubNotifications(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationHub>("notifications");
        return endpoints;
    }
}