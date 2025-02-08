using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Post;

public class SeenPost : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public PostId PostId { get; private set; }
    public DateTime SeenAt { get; private set; }

    private SeenPost()
    {
    }

    private SeenPost(Guid id, UserId userId, PostId postId) : base(id)
    {
        UserId = userId;
        PostId = postId;
        SeenAt = DateTime.UtcNow;
    }

    public static SeenPost Create(UserId userId, PostId postId)
    {
        return new SeenPost(Guid.NewGuid(), userId, postId);
    }
}