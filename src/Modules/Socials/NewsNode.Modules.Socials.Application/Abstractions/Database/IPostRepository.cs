using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetPostByIdAsync(PostId id, CancellationToken cancellationToken = default);
    Task<List<Post>> GetPostsByUserProfileIdAsync(UserId userProfileId, CancellationToken cancellationToken = default);
}