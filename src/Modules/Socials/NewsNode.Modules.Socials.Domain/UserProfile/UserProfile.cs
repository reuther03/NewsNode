using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    public Name UserName { get; private set; }
    public Email Email { get; private set; }

    private readonly List<UserProfileFollow> _profileFollows = [];
    public IReadOnlyList<UserProfileFollow> ProfileFollows => _profileFollows.AsReadOnly();

    private readonly List<UserProfileStatus> _profileStatuses = [];
    public IReadOnlyList<UserProfileStatus> ProfileStatuses => _profileStatuses.AsReadOnly();

    private UserProfile()
    {
    }

    private UserProfile(UserId id, Email email, Name userName) : base(id)
    {
        Email = email;
        UserName = userName;
    }

    public static UserProfile Create(Guid userId, Email email, Name userName)
        => new(UserId.From(userId), email, userName);

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
}