using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using UserId = NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids.UserId;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class UserProfile : AggregateRoot<UserId>
{
    private readonly List<UserProfileFollow> _profileFollows = [];
    private readonly List<UserProfileStatus> _profileStatuses = [];
    private readonly List<PostId> _repostedPosts = [];
    public Name UserName { get; private set; }
    public Email Email { get; private set; }

    public IReadOnlyList<UserProfileFollow> ProfileFollows => _profileFollows.AsReadOnly();
    public IReadOnlyList<UserProfileStatus> ProfileStatuses => _profileStatuses.AsReadOnly();
    public IReadOnlyList<PostId> RepostedPosts => _repostedPosts.AsReadOnly();

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

    public void Repost(PostId postId)
    {
        if (_repostedPosts.Contains(postId))
            throw new DomainException("You have already reposted this post");

        _repostedPosts.Add(postId);
    }
}