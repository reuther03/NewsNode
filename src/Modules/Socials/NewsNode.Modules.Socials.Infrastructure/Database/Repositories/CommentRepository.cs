using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class CommentRepository : Repository<Comment, SocialsDbContext>, ICommentRepository
{
    public CommentRepository(SocialsDbContext dbContext) : base(dbContext)
    {
    }
}