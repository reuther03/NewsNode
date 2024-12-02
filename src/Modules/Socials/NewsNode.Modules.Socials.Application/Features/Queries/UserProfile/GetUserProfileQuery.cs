using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Application.Features.Queries.Dtos;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Queries;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Queries.UserProfile;

public record GetUserProfileQuery(
    [property: JsonIgnore]
    Guid UserProfilId) : IQuery<UserProfileDto>
{
    internal sealed class Handler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly ISocialsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(ISocialsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            //chyba tego nie musi byc
            if (!_userService.IsAuthenticated)
                return Result.Unauthorized<UserProfileDto>("User is not authenticated");

            var userProfile = await _dbContext.UserProfiles.FindAsync([UserId.From(request.UserProfilId)], cancellationToken);
            NullValidator.ValidateNotNull(userProfile);

            var userProfileFollowingCount = await _dbContext.UserProfiles
                .Where(x => x.FollowIds.Any(z => z.Value == userProfile.Id.Value))
                .CountAsync(cancellationToken);

            var userProfileDto = new UserProfileDto
            {
                Id = userProfile.Id,
                UserName = userProfile.UserName,
                Email = userProfile.Email,
                FollowersCount = userProfile.FollowIds.Count,
                FollowingCount = userProfileFollowingCount
            };

            return Result.Ok(userProfileDto);
        }
    }
}