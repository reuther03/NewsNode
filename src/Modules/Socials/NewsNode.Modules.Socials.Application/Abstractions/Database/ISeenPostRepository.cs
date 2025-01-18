using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface ISeenPostRepository : IRepository<SeenPost>
{
    Task AddRangeAsync(IEnumerable<SeenPost> seenPosts, CancellationToken cancellationToken);
    bool Exists(UserId userId, PostId postId);
}