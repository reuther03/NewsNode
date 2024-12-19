using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileFollow : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public UserId TargetUserId { get; private set; }

    private UserProfileFollow()
    {
    }

    private UserProfileFollow(Guid id, UserId targetUserId) : base(id)
    {
        TargetUserId = targetUserId;
    }

    public static UserProfileFollow Create(UserId targetUserId)
        => new(Guid.NewGuid(), targetUserId);
}