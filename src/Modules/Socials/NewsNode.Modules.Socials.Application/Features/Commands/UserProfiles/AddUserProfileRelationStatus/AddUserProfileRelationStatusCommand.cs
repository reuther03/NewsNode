using System.Text.Json.Serialization;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;
using NewsNode.Shared.Abstractions.Services;
using ICommand = NewsNode.Shared.Abstractions.QueriesAndCommands.Commands.ICommand;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.MuteUserProfile;

public record AddUserProfileRelationStatusCommand(
    [property: JsonIgnore]
    Guid UserProfileId,
    UserProfileRelationStatus Status) : ICommand
{
    internal sealed class Handler : ICommandHandler<AddUserProfileRelationStatusCommand>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IFollowerRepository _followerRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserProfileRepository userProfileRepository, IFollowerRepository followerRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _userProfileRepository = userProfileRepository;
            _followerRepository = followerRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddUserProfileRelationStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userProfileRepository.GetFullByIdAsync(_userService.UserId, cancellationToken);
            NullValidator.ValidateNotNull(user);

            var targetUserProfile = await _userProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken);
            NullValidator.ValidateNotNull(targetUserProfile);

            if (request.Status == UserProfileRelationStatus.Blocked &&
                await _followerRepository.IsFollowingAsync(user.Id, targetUserProfile.Id, cancellationToken))
            {
                user.AddStatus(targetUserProfile.Id, request.Status);
                await _followerRepository.RemoveAsync(user.Id, targetUserProfile.Id, cancellationToken);
            }
            else
            {
                user.AddStatus(targetUserProfile.Id, request.Status);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}