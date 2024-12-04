using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Notifications.Hubs;
using NewsNode.Shared.Abstractions.Modules;

// ReSharper disable ClassNeverInstantiated.Global

namespace NewsNode.Services.Notifications;

public class NotificationsModule : IModule
{
    public const string BasePath = "notifications-module";

    public string Name => "notifications";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        services.AddNotifications();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints => endpoints.MapHubNotifications());
    }
}