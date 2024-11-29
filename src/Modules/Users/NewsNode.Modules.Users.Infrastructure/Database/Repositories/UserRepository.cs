using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Users.Infrastructure.Database.Repositories;

internal class UserRepository : Repository<User, UsersDbContext>, IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _context.Users.FindAsync([id], cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
}