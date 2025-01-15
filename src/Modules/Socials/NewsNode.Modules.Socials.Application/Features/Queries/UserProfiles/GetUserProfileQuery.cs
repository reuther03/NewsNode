using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.UserProfiles;

public record GetUserProfileQuery(
    [property: JsonIgnore]
    Guid UserProfilId) : IQuery<UserProfileDto>
{
    internal sealed class Handler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IRedisCacheService _redisCacheService;

        public Handler(ISocialsDbContext dbContext, IUserService userService, IRedisCacheService redisCacheService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _redisCacheService = redisCacheService;
        }

        public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated)
                return Result.Unauthorized<UserProfileDto>("User is not authenticated");

            var key = $"UserProfile:{request.UserProfilId}";

            var cachedDto = await _redisCacheService.GetDataAsync<UserProfileDto>(key);
            if (cachedDto != null)
                return Result.Ok(cachedDto);

            var userProfile = await _dbContext.UserProfiles
                .Include(x => x.ProfileFollows)
                .Include(x => x.ProfileStatuses)
                .Include(x => x.PostActions)
                .FirstOrDefaultAsync(x => x.Id == UserId.From(request.UserProfilId), cancellationToken);

            NullValidator.ValidateNotNull(userProfile);

            var userProfileFollowingCount = await _dbContext.UserProfiles
                .Where(x => x.ProfileFollows.Any(z => z.TargetUserId == userProfile.Id))
                .CountAsync(cancellationToken);

            var userProfilePostActions = userProfile.PostActions
                .Where(x => x.UserProfileId == userProfile.Id && x.ActionType == PostActionType.Reposted)
                .Select(x => x.PostId.Value)
                .ToList();

            var userProfileDto = new UserProfileDto
            {
                Id = userProfile.Id,
                UserName = userProfile.UserName,
                Email = userProfile.Email,
                FollowersCount = userProfile.ProfileFollows.Count,
                FollowingCount = userProfileFollowingCount,
                RepostedPosts = userProfilePostActions
            };

            await _redisCacheService.SetDataAsync(key, userProfileDto, TimeSpan.FromMinutes(5));

            return Result.Ok(userProfileDto);
        }
    }
}