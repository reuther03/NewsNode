using System.Data;
using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.FollowUserProfile;

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