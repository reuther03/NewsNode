using System.Text.Json.Serialization;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;
using ICommand = NewsNode.Shared.Abstractions.QueriesAndCommands.Commands.ICommand;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfile.MuteUserProfile;

public record MuteUserProfileCommand(
    [property: JsonIgnore]
    Guid UserProfileId) : ICommand
{
    internal sealed class Handler : ICommandHandler<MuteUserProfileCommand>
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

        public async Task<Result> Handle(MuteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userProfileRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var profileToMute = await _userProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken);
            NullValidator.ValidateNotNull(profileToMute);

            if (user.MutedUserProfileIds.Contains(profileToMute.Id))
                user.UnmuteUserProfile(profileToMute.Id);
            else
                user.MuteUserProfile(profileToMute.Id);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}