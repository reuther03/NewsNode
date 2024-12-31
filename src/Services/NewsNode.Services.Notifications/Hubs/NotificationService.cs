using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsNode.Services.Notifications.Database;
using NewsNode.Services.Notifications.Notifications;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Services.Notifications.Hubs;

public class NotificationService : INotificationService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public NotificationService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    //todo: moze zmienic jako jedna metoda a nie rozdzielac per typ notyfikacji
    public async Task FollowedNotification(Guid followerId, Guid followedProfileId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        var notification = Notification.Create(followedProfileId, nameof(FollowedNotification), $"{followerId} started following you.");

        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }

    public async Task PostNotification(List<UserId> followerIds, Guid userProfileId, Guid postId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();

        var notifications = followerIds.Select(followerId =>
            Notification.Create(followerId, nameof(PostNotification), $"{userProfileId} posted a new post {postId}.")
        ).ToList();

        await context.Notifications.AddRangeAsync(notifications);
        await context.SaveChangesAsync();
    }
}