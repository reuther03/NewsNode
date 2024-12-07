namespace NewsNode.Shared.Abstractions.Services;

public interface INotificationService
{
    //todo: zmienic jako jedna metoda a nie rozdzielac per typ notyfikacji
    Task FollowedNotification(Guid followerId, Guid followedProfileId);

}