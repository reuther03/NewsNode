using System.Text.Json.Serialization;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.FollowUserProfile;

public record FollowUserProfileCommand(
    [property: JsonIgnore]
    Guid UserProfileId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<FollowUserProfileCommand, Guid>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserService _userService;
        private readonly IRecommendationsService _recommendationsService;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;


        public Handler(IUserProfileRepository userProfileRepository,
            IUserService userService,
            IRecommendationsService recommendationsService,
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _userProfileRepository = userProfileRepository;
            _userService = userService;
            _recommendationsService = recommendationsService;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(FollowUserProfileCommand request, CancellationToken cancellationToken)
        {
            var follower = await _userProfileRepository.GetFullByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(follower);

            var profileToFollow = await _userProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken);
            NullValidator.ValidateNotNull(profileToFollow);

            var posts = await _postRepository.GetPostsByUserProfileIdAsync(profileToFollow.Id, cancellationToken);
            if (posts.Count == 0)
                return Result<Guid>.BadRequest("User has no posts");

            var mostInteractedHashtags = posts
                .Where(x => x.Hashtags.Count > 0)
                .OrderByDescending(x => x.Likes + x.Bookmarks + x.Reposts + x.Comments.Count)
                .SelectMany(x => x.Hashtags)
                .DistinctBy(x => x.Value)
                .Take(5)
                .ToList();

            follower.Follow(profileToFollow.Id);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _notificationService.FollowedNotification(follower.Id, profileToFollow.Id);
            await _recommendationsService.CreateActionRecommendation(follower.Id, mostInteractedHashtags, cancellationToken);

            return Result<Guid>.Ok(profileToFollow.Id);
        }
    }
}