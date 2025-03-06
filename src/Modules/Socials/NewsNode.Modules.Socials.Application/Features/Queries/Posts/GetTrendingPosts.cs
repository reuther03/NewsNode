using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetTrendingPosts(int Days = 14) : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetTrendingPosts, PaginatedList<PostDto>>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(ISocialsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetTrendingPosts request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles
                .Include(x => x.ProfileStatuses)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            List<Post> posts;
            if (user is not null)
            {
                posts = await _dbContext.Posts
                    .Where(x => x.PostedAt >= DateTime.UtcNow.AddDays(-request.Days))
                    .OrderByDescending(x => x.Likes)
                    .ThenByDescending(x => x.Reposts)
                    .ThenByDescending(x => x.Bookmarks)
                    .Where(x => !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == x.CreatedBy &&
                            y.Status == UserProfileRelationStatus.Muted ||
                            y.Status == UserProfileRelationStatus.Blocked))
                    .ToListAsync(cancellationToken);
            }
            else
            {
                posts = await _dbContext.Posts
                    .Where(x => x.PostedAt >= DateTime.UtcNow.AddDays(-request.Days))
                    .OrderByDescending(x => x.Likes)
                    .ThenByDescending(x => x.Reposts)
                    .ThenByDescending(x => x.Bookmarks)
                    .Where(x => !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == x.CreatedBy))
                    .ToListAsync(cancellationToken);
            }

            var commentsCount = await _dbContext.Comments
                .GroupBy(x => x.PostId)
                .Select(x => new { PostId = x.Key, Count = x.Count() })
                .ToDictionaryAsync(x => x.PostId, x => x.Count, cancellationToken);

            var postsDto = posts.Select(x => PostDto.AsDto(x, null, null, commentsCount.FirstOrDefault(z => z.Key == x.Id).Value)).ToList();

            return PaginatedList<PostDto>.Create(1, postsDto.Count, postsDto.Count, postsDto);
        }
    }
}