using MediatR;
using NewsNode.Modules.Socials.Application.Abstractions;
using NewsNode.Modules.Socials.Application.Abstractions.Database;
using NewsNode.Modules.Socials.Domain.UserProfile;
using NewsNode.Shared.Abstractions.Integration.Events.Users;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Modules.Socials.Application.Events.Integration;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedEventHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userProfile = UserProfile.Create(notification.UserId, notification.Email, notification.UserName);

        await _userProfileRepository.AddAsync(userProfile, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}