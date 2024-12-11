using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.Database;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface IFollowerRepository : IRepository<UserProfileFollower>
{
    Task RemoveAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default);
}