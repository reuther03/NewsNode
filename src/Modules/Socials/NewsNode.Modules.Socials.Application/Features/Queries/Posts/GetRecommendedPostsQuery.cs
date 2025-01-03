using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Pagination;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Extensions;
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

            var posts = await _dbContext.Posts
                .Include(x => x.Hashtags)
                .Where(x => !_dbContext.UserProfileStatuses.Any(y => y.UserId == user.Id && y.TargetUserId == x.CreatedBy))
                .OrderByDescending(x => _dbContext.Posts.Sum(y => y.Likes + y.Bookmarks + y.Reposts + y.Comments.Count))
                .ThenByDescending(x => x.PostedAt)
                .ToListAsync(cancellationToken);

            var postsDto = posts.Select(PostDto.AsDto).ToList();

            return PaginatedList<PostDto>.Create(1, postsDto.Count, postsDto.Count, postsDto);
        }
    }
}