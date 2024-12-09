using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileFollower : Entity<Guid>
{
    public UserId FollowerId { get; private set; }

    private UserProfileFollower()
    {
    }

    private UserProfileFollower(Guid id, UserId followerId) : base(id)
    {
        FollowerId = followerId;
    }

    public static UserProfileFollower Create(UserId followerId)
        => new(Guid.NewGuid(), followerId);
}