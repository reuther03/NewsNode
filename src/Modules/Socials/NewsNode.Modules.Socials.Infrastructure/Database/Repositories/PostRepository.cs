using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class PostRepository : Repository<Post, SocialsDbContext>, IPostRepository
{
    private readonly SocialsDbContext _dbContext;

    public PostRepository(SocialsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post?> GetPostByIdAsync(PostId id, CancellationToken cancellationToken = default)
        => await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Post>> GetPostsByUserProfileIdAsync(UserId userProfileId, CancellationToken cancellationToken = default)
        => await _dbContext.Posts
            .Where(x => x.CreatedBy == userProfileId)
            .ToListAsync(cancellationToken);
}