using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.UserProfile;

public class PostAction : Entity<Guid>
{
    public UserId UserProfileId { get; private set; }
    public PostId PostId { get; private set; }
    public PostActionType ActionType { get; private set; }

    private PostAction()
    {
    }

    private PostAction(Guid id, PostId postId, PostActionType actionType) : base(id)
    {
        PostId = postId;
        ActionType = actionType;
    }

    public static PostAction Create(PostId postId, PostActionType actionType)
        => new(Guid.NewGuid(), postId, actionType);
}