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
            .Include(x => x.Relations)
            .FirstOrDefaultAsync(x => x.Id == UserId.From(id), cancellationToken);

    public async Task<bool> IsFollowingAsync(Guid userProfileId, Guid targetUserProfileId, CancellationToken cancellationToken = default)
        => await _context.UserProfiles
            .Where(x => x.Id == UserId.From(userProfileId))
            .AnyAsync(x => x.Relations.Any(y => y.TargetUserId == UserId.From(targetUserProfileId) &&
                y.Status == UserProfileRelationStatus.Followed), cancellationToken);

    // public async Task<List<UserId>> GetFollowersWhereUnMutedAsync(Guid profileId, CancellationToken cancellationToken = default)
    //     => await _context.UserProfiles
    //         .Where(x => x.ProfileRelations.Any(z => z.TargetUserProfileId == UserId.From(profileId) && z.RelationStatus == UserProfileRelationStatus.None))
    //         .Select(x => x.Id)
    //         .ToListAsync(cancellationToken);
}