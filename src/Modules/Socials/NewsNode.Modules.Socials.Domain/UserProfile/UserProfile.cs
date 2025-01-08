using NewsNode.Shared.Abstractions.Events.Domain.Posts;
using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    private readonly List<UserProfileFollow> _profileFollows = [];
    private readonly List<UserProfileStatus> _profileStatuses = [];
    private readonly List<PostAction> _postActions = [];
    public Name UserName { get; private set; }

    public Email Email { get; private set; }

    // public string Bio { get; private set; }
    public Location Location { get; private set; }

    public IReadOnlyList<UserProfileFollow> ProfileFollows => _profileFollows.AsReadOnly();
    public IReadOnlyList<UserProfileStatus> ProfileStatuses => _profileStatuses.AsReadOnly();
    public IReadOnlyList<PostAction> PostActions => _postActions.AsReadOnly();

    private UserProfile()
    {
    }

    private UserProfile(UserId id, Email email, Name userName, Location location) : base(id)
    {
        Email = email;
        UserName = userName;
        Location = location;
    }

    public static UserProfile Create(Guid userId, Email email, Name userName, Location location) =>
        new(UserId.From(userId), email, userName, location);

    public void Follow(UserId targetUserId)
    {
        if (_profileFollows.Any(x => x.TargetUserId == targetUserId))
            throw new DomainException("User is already following this user");

        _profileFollows.Add(UserProfileFollow.Create(targetUserId));
    }

    public void AddStatus(UserId targetUserId, UserProfileRelationStatus status)
    {
        if (_profileStatuses.Any(x => x.TargetUserId == targetUserId && x.Status == status))
            throw new DomainException("User is already in this relation");

        _profileStatuses.Add(UserProfileStatus.Create(targetUserId, status));
    }

    public void AddPostAction(PostId postId, PostActionType actionType)
    {
        if (_postActions.Any(x => x.PostId == postId && x.ActionType == actionType))
            throw new DomainException("User already performed this action");

        _postActions.Add(PostAction.Create(postId, actionType));

        RaiseDomainEvent(new ActionPerformedEvent(postId, actionType));
    }
}