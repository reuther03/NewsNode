using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Kernel.Database;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Users.Application.Abstractions.Database;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default);
}