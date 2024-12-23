using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Abstractions.Database;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetFullByIdAsync(Guid id, CancellationToken cancellationToken = default);
}