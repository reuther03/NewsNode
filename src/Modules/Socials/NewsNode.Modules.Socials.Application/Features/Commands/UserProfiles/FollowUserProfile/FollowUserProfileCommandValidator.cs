using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.FollowUserProfile;

public class FollowUserProfileCommandValidator : AbstractValidator<FollowUserProfileCommand>
{
    public FollowUserProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotNull();
    }
}