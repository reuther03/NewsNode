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

        public Handler(IRecommendationsService recommendations, IUserService userService, ISocialsDbContext dbContext)
        {
            _recommendations = recommendations;
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetRecommendedPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var recommendedHashtags = await _recommendations.GetRecommendedHashtags(user.Id, cancellationToken);
            var recommendedProfiles = await _recommendations.GetRecommendedProfiles(user.Id, cancellationToken);

            var posts = await _dbContext.Posts
                .Include(p => p.Hashtags)
                .Where(p => !_dbContext.UserProfileStatuses.Any(y =>
                    y.UserId == user.Id &&
                    y.TargetUserId == p.CreatedBy))
                .ToListAsync(cancellationToken);

            var filteredPosts = posts
                .Where(p => p.Hashtags.Any(h => recommendedHashtags.Contains(h.Value)))
                .OrderByDescending(p => p.Likes + p.Bookmarks + p.Reposts + p.Comments.Count)
                .ThenByDescending(p => p.PostedAt)
                .ToList();

            var postsDto = filteredPosts.Select(PostDto.AsDto).ToList();

            return PaginatedList<PostDto>.Create(1, postsDto.Count, postsDto.Count, postsDto);
        }
    }
}