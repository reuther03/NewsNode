using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    public Name UserName { get; private set; }
    public Email Email { get; private set; }

    private readonly List<UserProfileRelation> _relations = [];
    public IReadOnlyList<UserProfileRelation> Relations => _relations.AsReadOnly();

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


    public void AddRelation(UserProfileRelation relation)
    {
        if (_relations.Exists(x => x.TargetUserId == relation.TargetUserId && x.Status == relation.Status))
            throw new DomainException("Relation already exists");

        if (_relations.Any(x => x.TargetUserId == Id))
            throw new DomainException("Cannot add self relation");

        _relations.Add(relation);
    }
}