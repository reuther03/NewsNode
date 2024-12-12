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
            .Include(x => x.Followers)
            .Include(x => x.ProfileRelations)
            .FirstOrDefaultAsync(x => x.Id == UserId.From(id), cancellationToken);

    public async Task<bool> IsFollowingAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default)
        => await _context.UserProfiles.Where(x => x.Id == UserId.From(followerId))
            .AnyAsync(x => x.Followers.Any(y => y.FollowerId == UserId.From(followedProfileId)), cancellationToken);

    public async Task<List<UserId>> GetFollowersWhereUnMutedAsync(Guid profileId, CancellationToken cancellationToken = default)
        => await _context.UserProfiles
            .Where(x => x.Id == UserId.From(profileId) &&
                x.ProfileRelations.Any(z => z.RelationStatus == UserProfileRelationStatus.None))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
}