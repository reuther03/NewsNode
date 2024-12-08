using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfile.FollowUserProfile;

public class FollowUserProfileCommandValidator : AbstractValidator<FollowUserProfileCommand>
{
    public FollowUserProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotNull();

        RuleFor(x => x.Mute)
            .NotNull();
    }
}