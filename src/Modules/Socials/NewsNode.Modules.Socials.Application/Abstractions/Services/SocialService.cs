using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Abstractions.Services;

public class SocialService : ISocialService
{
    private readonly IUserProfileRepository _userProfileRepository;

    public SocialService(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    // public Task<bool> IsFollowingAsync(Guid followerId, Guid followedProfileId, CancellationToken cancellationToken = default)
    //     => _userProfileRepository.IsFollowingAsync(followerId, followedProfileId, cancellationToken);
}