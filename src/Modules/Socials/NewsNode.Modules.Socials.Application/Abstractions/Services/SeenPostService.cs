using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Abstractions.Services;

public class SeenPostService : ISeenPostService
{
    private readonly ISeenPostRepository _seenPostRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SeenPostService(ISeenPostRepository seenPostRepository, IUnitOfWork unitOfWork)
    {
        _seenPostRepository = seenPostRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task MarkAsSeenAsync(UserId userId, List<PostId> postIds, CancellationToken cancellationToken = default)
    {
        var seenPosts = postIds.Select(postId => SeenPost.Create(userId, postId)).ToList();
        await _seenPostRepository.AddRangeAsync(seenPosts, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}