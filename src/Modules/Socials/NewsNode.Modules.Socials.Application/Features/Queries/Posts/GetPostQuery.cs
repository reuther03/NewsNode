using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Posts;

public record GetPostQuery(
    [property: JsonIgnore]
    Guid CurrentPostId) : IQuery<PostDetailsDto>
{
    internal sealed class Handler : IQueryHandler<GetPostQuery, PostDetailsDto>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(ISocialsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PostDetailsDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.UserProfiles
                .Include(x => x.ProfileStatuses)
                .FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            NullValidator.ValidateNotNull(user);

            var post = await _dbContext.Posts
                .Include(x => x.Hashtags)
                .FirstOrDefaultAsync(x => x.Id == PostId.From(request.CurrentPostId), cancellationToken);

            NullValidator.ValidateNotNull(post);

            if (await _dbContext.UserProfileStatuses.AnyAsync(x => x.TargetUserId == post.CreatedBy &&
                    x.Status == UserProfileRelationStatus.Blocked, cancellationToken))
                return Result<PostDetailsDto>.BadRequest("User is blocked");

            var postActions = await _dbContext.PostActions
                .Where(x => x.PostId == post.Id)
                .Select(x => new { x.ActionType, x.UserProfileId })
                .ToListAsync(cancellationToken);

            var postDetails = new PostDetailsDto
            {
                Id = post.Id,
                Content = post.Content,
                CreatedBy = post.CreatedBy,
                PostedAt = post.PostedAt,
                LikeIds = postActions.Where(x => x.ActionType == PostActionType.Liked).Select(x => x.UserProfileId.Value).ToList(),
                RepostIds = postActions.Where(x => x.ActionType == PostActionType.Reposted).Select(x => x.UserProfileId.Value).ToList(),
                Hashtags = post.Hashtags.Select(x => x.Value).ToList(),
                Comments = await _dbContext.Comments.Where(x => x.PostId == post.Id).Select(x => CommentDto.AsDto(x)).ToListAsync(cancellationToken)
            };

            return Result.Ok(postDetails);
        }
    }
}