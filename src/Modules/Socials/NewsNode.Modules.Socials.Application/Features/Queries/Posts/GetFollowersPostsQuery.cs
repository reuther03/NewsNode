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
using StackExchange.Redis;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetFollowersPostsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<PostDto>>
{
    internal sealed class Handler : IQueryHandler<GetFollowersPostsQuery, PaginatedList<PostDto>>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(ISocialsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<PostDto>>> Handle(GetFollowersPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles
                .Include(x => x.ProfileFollows)
                .Include(x => x.ProfileStatuses)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            NullValidator.ValidateNotNull(user);

            var followedProfiles = await _dbContext.UserProfileFollowers
                .Where(x => x.UserId == user.Id)
                .Select(x => x.TargetUserId)
                .ToListAsync(cancellationToken);

            var posts = await _dbContext.Posts
                .Where(x => followedProfiles.Contains(x.CreatedBy))
                .Where(x => !_dbContext.UserProfileStatuses
                    .Any(y => y.TargetUserId == x.CreatedBy &&
                        y.Status == UserProfileRelationStatus.Muted))
                .OrderByDescending(x => x.PostedAt)
                .ToPagedListAsync<Post, PostDto>(request.Page, request.PageSize, x => PostDto.AsDto(x), cancellationToken);

            return Result.Ok(posts);
        }
    }
}