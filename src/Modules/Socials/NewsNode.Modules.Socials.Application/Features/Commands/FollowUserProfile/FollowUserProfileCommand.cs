using System.Text.Json.Serialization;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;

namespace NewsNode.Modules.Socials.Application.Features.Commands.FollowUserProfile;

public record FollowUserProfileCommand(
    [property: JsonIgnore]
    Guid UserProfileId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<FollowUserProfileCommand, Guid>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;


        public Handler(IUserProfileRepository userProfileRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _userProfileRepository = userProfileRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(FollowUserProfileCommand request, CancellationToken cancellationToken)
        {
            var follower = await _userProfileRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(follower);

            var profileToFollow = await _userProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken);
            NullValidator.ValidateNotNull(profileToFollow);

            profileToFollow.Follow(follower.Id);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Ok(profileToFollow.Id);
        }
    }
}