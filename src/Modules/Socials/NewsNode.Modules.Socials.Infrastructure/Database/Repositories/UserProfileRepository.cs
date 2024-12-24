using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class UserProfileRepository : Repository<UserProfile, SocialsDbContext>, IUserProfileRepository
{
    private readonly SocialsDbContext _context;

    public UserProfileRepository(SocialsDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<UserProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.UserProfiles
            .FirstOrDefaultAsync(x => x.Id == UserId.From(id), cancellationToken);

    public async Task<UserProfile?> GetFullByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.UserProfiles
            .Include(x => x.ProfileFollows)
            .Include(x => x.ProfileStatuses)
            .Include(x => x.PostActions)
            .FirstOrDefaultAsync(x => x.Id == UserId.From(id), cancellationToken);
}