using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfileRelation : Entity<Guid>
{
    public UserId TargetUserProfileId { get; private set; }
    public UserProfileRelationStatus RelationStatus { get; private set; }

    private UserProfileRelation()
    {
    }

    private UserProfileRelation(Guid id, UserId targetUserProfileId, UserProfileRelationStatus relationStatus) : base(id)
    {
        TargetUserProfileId = targetUserProfileId;
        RelationStatus = relationStatus;
    }

    public static UserProfileRelation Create(UserId targetUserProfileId, UserProfileRelationStatus? relationStatus = null)
        => new(Guid.NewGuid(), targetUserProfileId, relationStatus ?? UserProfileRelationStatus.None);
}