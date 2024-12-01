using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    private readonly List<UserId> _followers = [];

    public Name UserName { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyList<UserId> Followers => _followers;

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

        if (_followers.Contains(userId))
            throw new DomainException("User is already followed");


        _followers.Add(userId);
    }
}