using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileRelation : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public UserId TargetUserId { get; private set; }
    public UserProfileRelationStatus Status { get; private set; }

    private UserProfileRelation()
    {
    }

    private UserProfileRelation(Guid id, UserId targetUserId, UserProfileRelationStatus relationStatus) : base(id)
    {
        TargetUserId = targetUserId;
        Status = relationStatus;
    }

    public static UserProfileRelation Create(UserId targetUserId, UserProfileRelationStatus relationStatus)
        => new(Guid.NewGuid(), targetUserId, relationStatus);
}