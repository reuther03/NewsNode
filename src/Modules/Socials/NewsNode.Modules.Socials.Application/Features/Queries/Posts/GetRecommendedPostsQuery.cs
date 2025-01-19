using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetRecommendedPostsQuery : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetRecommendedPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IRecommendationsService _recommendations;
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly ISeenPostService _seenPostService;

        public Handler(IRecommendationsService recommendations, IUserService userService, ISocialsDbContext dbContext, ISeenPostService seenPostService)
        {
            _recommendations = recommendations;
            _userService = userService;
            _dbContext = dbContext;
            _seenPostService = seenPostService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetRecommendedPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles
                .Include(x => x.SeenPosts)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var recommendedHashtags = await _recommendations.GetRecommendedHashtags(user.Id, cancellationToken);
            var recommendedProfiles = await _recommendations.GetRecommendedProfiles(user.Id, cancellationToken);

            var seenPostsIds = await _dbContext.SeenPosts
                .Where(x => x.UserId == user.Id)
                .Select(x => x.PostId)
                .ToHashSetAsync(cancellationToken);

            var posts = await _dbContext.Posts
                .Where(p => recommendedProfiles.Contains(p.CreatedBy) ||
                    recommendedHashtags.Select(x => x.Value)
                        .Intersect(p.Hashtags.Select(y => y.Value)).Any() &&
                    p.CreatedBy != user.Id &&
                    !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == p.CreatedBy))
                .Select(x => new { Post = x, Seen = seenPostsIds.Contains(x.Id) })
                .ToListAsync(cancellationToken);

            var unseenPostsDto = posts.Where(p => !p.Seen).Select(p => PostDto.AsDto(p.Post, false)).ToList();
            var seenPostsDto = posts.Where(p => p.Seen).Select(p => PostDto.AsDto(p.Post, true)).ToList();

            var allPostsDto = unseenPostsDto
                .Concat(seenPostsDto)
                .OrderBy(x => x.Seen)
                .ToList();

            await _seenPostService.MarkAsSeenAsync(user.Id, posts.Select(x => x.Post.Id).ToList(), cancellationToken);

            return PaginatedList<PostDto>.Create(1, allPostsDto.Count, allPostsDto.Count, allPostsDto);
        }
    }
}