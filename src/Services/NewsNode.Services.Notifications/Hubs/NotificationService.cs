using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Notifications.Database;
using NewsNode.Services.Notifications.Notifications;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications.Hubs;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public NotificationService(IHubContext<NotificationHub> hubContext, IServiceScopeFactory serviceScopeFactory, IUserService userService)
    {
        _hubContext = hubContext;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task FollowedNotification(Guid followerId, Guid followedProfileId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        var notification = Notification.Create(followedProfileId, nameof(FollowedNotification), $"{followerId} started following you.");

        await _hubContext.Clients.User(followedProfileId.ToString()).SendAsync("FollowedNotification", $" {followerId} started following you.");
        notification.MarkAsSent();
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }
}