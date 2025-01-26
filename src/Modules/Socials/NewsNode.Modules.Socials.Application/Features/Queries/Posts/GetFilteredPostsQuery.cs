using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public class GetFilteredPostsQuery : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetFilteredPostsQuery, PaginatedList<PostDto>>
    {
        public async Task<Result<PaginatedList<PostDto>>> Handle(GetFilteredPostsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}