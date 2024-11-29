using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Users.Infrastructure.Database.Repositories;

internal class UserRepository : Repository<User, UsersDbContext>, IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
}