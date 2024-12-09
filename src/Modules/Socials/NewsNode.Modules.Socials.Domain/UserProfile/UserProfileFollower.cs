using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileFollower : Entity<Guid>
{
    public UserId FollowerId { get; private set; }
    public bool IsUserProfileMuted { get; private set; }

    private UserProfileFollower()
    {
    }

    private UserProfileFollower(Guid id, UserId followerId, bool isUserProfileMuted) : base(id)
    {
        FollowerId = followerId;
        IsUserProfileMuted = isUserProfileMuted;
    }

    public static UserProfileFollower Create(UserId followerId, bool isUserProfileMuted)
        => new(Guid.NewGuid(), followerId, isUserProfileMuted);
}