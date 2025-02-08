using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Notifications.Database;
using NewsNode.Services.Notifications.Hubs;
using NewsNode.Services.Notifications.Notifications;
using NewsNode.Shared.Abstractions.Services;
using NewsNode.Shared.Infrastructure.Postgres;
using NewsNode.Shared.Infrastructure.Services;

namespace NewsNode.Services.Notifications;

public static class Extensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddPostgres<NotificationsDbContext>();
        services.AddScoped<NotificationsDbContext>();
        services.AddSingleton<IHubConnectionService, HubConnectionService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddHostedService<SendNotificationsJob>();
        services.AddSignalR();
        return services;
    }

    public static IEndpointRouteBuilder MapHubNotifications(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationHub>("notifications");
        return endpoints;
    }
}