using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class SeenPostRepository : Repository<SeenPost, SocialsDbContext>, ISeenPostRepository
{
    private readonly SocialsDbContext _dbContext;

    public SeenPostRepository(SocialsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<SeenPost> seenPosts, CancellationToken cancellationToken)
        => await _dbContext.SeenPosts.AddRangeAsync(seenPosts, cancellationToken);

    public bool Exists(UserId userId, PostId postId)
        => _dbContext.SeenPosts.Any(x => x.UserId == userId && x.PostId == postId);
}