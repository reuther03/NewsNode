using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class FollowerRepository : Repository<UserProfileFollow, SocialsDbContext>, IFollowerRepository
{
    private readonly SocialsDbContext _dbContext;

    public FollowerRepository(SocialsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsFollowingAsync(Guid userProfileId, Guid targetUserProfileId, CancellationToken cancellationToken = default)
        => await _dbContext.UserProfileFollowers
            .AnyAsync(x => x.UserId == UserId.From(userProfileId) &&
                x.TargetUserId == UserId.From(targetUserProfileId), cancellationToken);

    public async Task RemoveAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.UserProfileFollowers
            .FirstOrDefaultAsync(
                x => x.UserId == UserId.From(followerId) && x.TargetUserId == UserId.From(followedProfileId), cancellationToken);

        if (entity is null)
            return;

        _dbContext.UserProfileFollowers.Remove(entity);
    }
}