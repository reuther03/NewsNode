using System.Text.Json.Serialization;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;


        public Handler(IUserProfileRepository userProfileRepository, IUserService userService, IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _userProfileRepository = userProfileRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(FollowUserProfileCommand request, CancellationToken cancellationToken)
        {
            // var follower = await _userProfileRepository.GetFullByIdAsync(_userService.UserId, cancellationToken);
            // NullValidator.ValidateNotNull(follower);
            //
            // var profileToFollow = await _userProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken);
            // NullValidator.ValidateNotNull(profileToFollow);
            //
            // follower.AddRelation(profileToFollow.Id, UserProfileRelationStatus.Followed);
            //
            // await _unitOfWork.CommitAsync(cancellationToken);
            // await _notificationService.FollowedNotification(follower.Id, profileToFollow.Id);
            //
            // return Result<Guid>.Ok(profileToFollow.Id);
            return Result<Guid>.Ok(Guid.NewGuid());
        }
    }
}