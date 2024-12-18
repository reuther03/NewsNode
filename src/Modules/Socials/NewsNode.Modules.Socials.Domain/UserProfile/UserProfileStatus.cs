using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileStatus : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public UserId TargetUserId { get; private set; }
    public UserProfileRelationStatus Status { get; private set; }

    private UserProfileStatus()
    {
    }

    private UserProfileStatus(Guid id, UserId userId, UserProfileRelationStatus status) : base(id)
    {
        UserId = userId;
        Status = status;
    }

    public static UserProfileStatus Create(UserId userId, UserProfileRelationStatus status)
        => new(Guid.NewGuid(), userId, status);
}