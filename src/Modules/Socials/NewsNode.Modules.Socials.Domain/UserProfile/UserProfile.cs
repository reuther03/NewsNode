using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    public Name UserName { get; private set; }
    public Email Email { get; private set; }


    //moze zmienic to nazwe
    private readonly List<UserId> _followIds = [];
    public IReadOnlyList<UserId> FollowIds => _followIds.AsReadOnly();

    private readonly List<UserId> _mutedUserProfileIds = [];
    public IReadOnlyList<UserId> MutedUserProfileIds => _mutedUserProfileIds.AsReadOnly();

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

    public void Follow(UserId userId)
    {
        if (userId == Id)
            throw new DomainException("You can't follow yourself");

        if (_followIds.Contains(userId))
            throw new DomainException("User is already followed");

        _followIds.Add(userId);
    }

    public void MuteUserProfile(UserId userId)
    {
        if (userId == Id)
            throw new DomainException("You can't mute yourself");

        if (_mutedUserProfileIds.Contains(userId))
            throw new DomainException("User is already muted");

        _mutedUserProfileIds.Add(userId);
    }
}