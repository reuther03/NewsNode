using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfile.MuteUserProfile;

public class MuteUserProfileCommandValidator : AbstractValidator<MuteUserProfileCommand>
{
    public MuteUserProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotEmpty();
    }
}