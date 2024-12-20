using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface IFollowerRepository : IRepository<UserProfileFollow>
{
    Task<bool> IsFollowingAsync(Guid userProfileId, Guid targetUserProfileId, CancellationToken cancellationToken = default);
    Task<List<UserId>> GetFollowersWhereUnMutedAsync(Guid profileId, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default);
}