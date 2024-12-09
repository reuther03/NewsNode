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

    public async Task<bool> IsFollowingAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default)
        => await _context.UserProfiles
            .AnyAsync(x => x.Id.Value == followedProfileId &&
                x.ProfileFollowers.Any(z => z.FollowerId.Value == followerId), cancellationToken);

    // public async Task<List<UserId>> GetFollowedProfilesAsync(Guid profileId, CancellationToken cancellationToken = default)
    // {
    //     var result = await _context.UserProfiles
    //         .Where(x => x.FollowIds.Any(y => y.Value == profileId)) // Use Any instead of Contains
    //         .Select(x => x.Id) // Only select needed data
    //         .ToListAsync(cancellationToken);
    //
    //     return result;
    // }

    // public async Task<List<UserId>> GetFollowersWhereUnMutedAsync(Guid profileId, CancellationToken cancellationToken = default)
    // {
    //     var targetUser = await _context.UserProfiles
    //         .AsNoTracking()
    //         .Where(x => x.Id == UserId.From(profileId))
    //         .Select(x => new
    //         {
    //             x.FollowIds // Get the followers of the target user
    //         })
    //         .FirstOrDefaultAsync(cancellationToken);
    //
    //     if (targetUser == null)
    //     {
    //         return new List<UserId>(); // If target user doesn't exist, return empty list
    //     }
    //
    //     // Step 2: Fetch profiles of the followers
    //     var unMutedFollowers = await _context.UserProfiles
    //         .Where(x => targetUser.FollowIds.Any(z => z == x.Id)) // Only profiles that follow the target user
    //         .Where(x => !x.MutedUserProfileIds.Any(z => z == UserId.From(profileId))) // Exclude those who muted the target user
    //         .Select(x => x.Id) // Get their IDs
    //         .ToListAsync(cancellationToken);
    //
    //     return unMutedFollowers;
    // }
}