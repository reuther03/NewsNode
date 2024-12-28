using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Domain.Recommendation;

public class Recommendation : Entity<Guid>
{
    public UserId UserProfileId { get; private set; }
    public Hashtag Hashtag { get; private set; }
    public int ActionCount { get; private set; }
}