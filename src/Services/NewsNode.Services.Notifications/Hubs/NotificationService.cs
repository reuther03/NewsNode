using Microsoft.AspNetCore.SignalR;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications.Hubs;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task FollowedNotification(Guid followerId, Guid followedProfileId)
    {
        return _hubContext.Clients.User(followedProfileId.ToString()).SendAsync("FollowedNotification", $" {followerId} started following you.");
    }
}