using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Services;

public interface ISeenPostService
{
    Task MarkAsSeenAsync(UserId userId, List<PostId> postIds, CancellationToken cancellationToken = default);
}