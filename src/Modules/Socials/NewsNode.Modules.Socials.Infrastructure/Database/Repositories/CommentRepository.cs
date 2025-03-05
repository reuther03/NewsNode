using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class CommentRepository : Repository<Comment, SocialsDbContext>, ICommentRepository
{
    private readonly SocialsDbContext _dbContext;

    public CommentRepository(SocialsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetCommentCountByPostId(PostId postId)
        => _dbContext.Comments.Count(x => x.PostId == postId);
}