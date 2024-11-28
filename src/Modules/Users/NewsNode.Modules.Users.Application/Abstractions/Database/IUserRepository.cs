using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Kernel.Database;

namespace NewsNode.Modules.Users.Application.Abstractions.Database;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}