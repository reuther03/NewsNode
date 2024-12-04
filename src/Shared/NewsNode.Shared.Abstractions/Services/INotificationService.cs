namespace NewsNode.Shared.Abstractions.Services;

public interface INotificationService
{
    Task FollowedNotification(Guid followerId, Guid followedProfileId);
}