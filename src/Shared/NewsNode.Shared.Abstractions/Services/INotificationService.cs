using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Services;

public interface INotificationService
{
    //todo: zmienic jako jedna metoda a nie rozdzielac per typ notyfikacji
    Task FollowedNotification(Guid followerId, Guid followedProfileId);

    Task PostNotification(List<UserId> followerIds, Guid userProfileId, Guid postId);
}