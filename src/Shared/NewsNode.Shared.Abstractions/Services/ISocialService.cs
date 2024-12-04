namespace NewsNode.Shared.Abstractions.Services;

public interface ISocialService
{
    Task<bool> IsFollowingAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default);
}