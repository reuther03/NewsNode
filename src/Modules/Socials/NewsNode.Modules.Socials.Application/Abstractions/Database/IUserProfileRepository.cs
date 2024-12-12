using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserProfile?> GetFullByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsFollowingAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default);

    Task<List<UserId>> GetFollowersWhereUnMutedAsync(Guid profileId, CancellationToken cancellationToken = default);
}