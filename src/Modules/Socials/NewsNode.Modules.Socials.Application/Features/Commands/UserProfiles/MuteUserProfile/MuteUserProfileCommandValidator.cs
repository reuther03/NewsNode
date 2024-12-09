using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.MuteUserProfile;

public class MuteUserProfileCommandValidator : AbstractValidator<MuteUserProfileCommand>
{
    public MuteUserProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotEmpty();
    }
}