using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetRecommendedPostsQuery(int Page = 1) : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetRecommendedPostsQuery, PaginatedList<PostDto>>
    {
        private readonly IRecommendationsService _recommendations;
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly ISeenPostService _seenPostService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IAIChatService _aiChatService;

        public Handler(IRecommendationsService recommendations, IUserService userService, ISocialsDbContext dbContext, ISeenPostService seenPostService,
            IRedisCacheService redisCacheService, IAIChatService aiChatService)
        {
            _recommendations = recommendations;
            _userService = userService;
            _dbContext = dbContext;
            _seenPostService = seenPostService;
            _redisCacheService = redisCacheService;
            _aiChatService = aiChatService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetRecommendedPostsQuery request, CancellationToken cancellationToken)
        {
            const string trendingCacheKey = "GlobalTrendingPosts";

            var user = await _dbContext.UserProfiles
                .Include(x => x.SeenPosts)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            NullValidator.ValidateNotNull(user);

            var recommendedHashtags = await _recommendations.GetRecommendedHashtags(user.Id, cancellationToken);
            var lessInterestedHashtags = await _recommendations.GetLessInterestedHashtags(user.Id, cancellationToken);
            var recommendedProfiles = await _recommendations.GetRecommendedProfiles(user.Id, cancellationToken);

            var seenPostsIds = await _dbContext.SeenPosts
                .Where(x => x.UserId == user.Id)
                .Select(x => x.PostId)
                .ToHashSetAsync(cancellationToken);

            var posts = await _dbContext.Posts
                .AsNoTracking()
                .Where(p => recommendedProfiles.Contains(p.CreatedBy) ||
                    recommendedHashtags.Select(x => x.Key.Value)
                        .Intersect(p.Hashtags.Select(y => y.Value)).Any() &&
                    lessInterestedHashtags.Select(x => x.Key.Value)
                        .Intersect(p.Hashtags.Select(y => y.Value)).Any() &&
                    p.CreatedBy != user.Id &&
                    !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == p.CreatedBy))
                .ToListAsync(cancellationToken);

            var cachedTrendingPosts = await _redisCacheService.GetDataAsync<List<PostDto>>(trendingCacheKey);
            if (cachedTrendingPosts == null)
            {
                cachedTrendingPosts = await _dbContext.Posts
                    .Where(p => p.PostedAt > DateTime.UtcNow.AddDays(-7))
                    .OrderByDescending(p => p.Likes + p.Bookmarks + p.Reposts)
                    .Take(25)
                    .Select(p => PostDto.AsDto(p, true, RecommendationWeight.None))
                    .ToListAsync(cancellationToken);

                await _redisCacheService.SetDataAsync(trendingCacheKey, cachedTrendingPosts, TimeSpan.FromMinutes(5));
            }

            var filteredTrendingPostsDto = cachedTrendingPosts
                .Where(p => p.CreatedBy != user.Id.Value &&
                    !_dbContext.UserProfileStatuses
                        .Any(y => y.TargetUserId == UserId.From(p.CreatedBy)))
                .ToList();

            var postsWithWeights = posts.Select(x => new
            {
                Post = x,
                Seen = seenPostsIds.Contains(x.Id),
                Weight = recommendedHashtags.FirstOrDefault(h => x.Hashtags.Contains(h.Key)).Value
            }).ToList();

            var unseenPostsDto = postsWithWeights.Where(p => !p.Seen).Select(p => PostDto.AsDto(p.Post, false, p.Weight)).ToList();
            var seenPostsDto = postsWithWeights.Where(p => p.Seen).Select(p => PostDto.AsDto(p.Post, true, p.Weight)).ToList();

            var allPostsDto = unseenPostsDto
                .Union(seenPostsDto)
                .Union(filteredTrendingPostsDto)
                .Distinct()
                .OrderBy(x => x.Seen)
                .ThenByDescending(x => x.Weight)
                .Skip((request.Page - 1) * 10)
                .Take(10)
                .ToList();

            await _seenPostService.MarkAsSeenAsync(user.Id, postsWithWeights.Select(x => x.Post.Id).ToList(), cancellationToken);

            return PaginatedList<PostDto>.Create(request.Page, 10, allPostsDto.Count, allPostsDto);
        }
    }
}