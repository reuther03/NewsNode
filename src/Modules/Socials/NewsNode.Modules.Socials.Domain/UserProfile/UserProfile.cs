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
    private readonly List<UserProfileFollower> _profileFollowers = [];
    public IReadOnlyList<UserProfileFollower> ProfileFollowers => _profileFollowers.AsReadOnly();

    private readonly List<UserProfileRelation> _profileRelations = [];
    public IReadOnlyList<UserProfileRelation> ProfileRelations => _profileRelations.AsReadOnly();

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

    public void AddFollower(UserId followerId)
    {
        if (_profileFollowers.Exists(x => x.FollowerId == followerId))
            throw new DomainException("Follower already exists");

        _profileFollowers.Add(UserProfileFollower.Create(followerId));
    }

    public void RemoveFollower(UserId followerId)
    {
        var follower = _profileFollowers.Find(x => x.FollowerId == followerId);
        if (follower is null)
            throw new DomainException("Follower not found");

        _profileFollowers.Remove(follower);
    }

    public void AddRelation(UserId targetUserProfileId, UserProfileRelationStatus? relationStatus)
    {
        if (_profileRelations.Exists(x => x.TargetUserProfileId == targetUserProfileId))
            throw new DomainException("Relation already exists");

        _profileRelations.Add(UserProfileRelation.Create(targetUserProfileId, relationStatus));
    }
}