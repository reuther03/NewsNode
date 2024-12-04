using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications.Hubs;

public static class Extensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSignalR();
        return services;
    }

    public static IEndpointRouteBuilder MapHubNotifications(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationHub>("notifications");
        return endpoints;
    }
}