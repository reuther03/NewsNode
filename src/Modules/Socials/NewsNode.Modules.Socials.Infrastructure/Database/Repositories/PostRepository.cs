using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.Article;
using NewsNode.Shared.Infrastructure.Postgres;

namespace NewsNode.Modules.Socials.Infrastructure.Database.Repositories;

internal class PostRepository : Repository<Post, SocialsDbContext>, IPostRepository
{
    public PostRepository(SocialsDbContext dbContext) : base(dbContext)
    {
    }
}