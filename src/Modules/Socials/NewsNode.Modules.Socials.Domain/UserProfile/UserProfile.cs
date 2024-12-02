using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    public Name UserName { get; private set; }
    public Email Email { get; private set; }

    public int Followers
    {
        get => _followerIds.Count;
        private set { } //for EF Core
    }


    //moze zmienic to nazwe
    private readonly List<UserId> _followerIds = [];
    public IReadOnlyList<UserId> FollowerIds => _followerIds.AsReadOnly();

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

        if (_followerIds.Contains(userId))
            throw new DomainException("User is already followed");

        _followerIds.Add(userId);
    }
}