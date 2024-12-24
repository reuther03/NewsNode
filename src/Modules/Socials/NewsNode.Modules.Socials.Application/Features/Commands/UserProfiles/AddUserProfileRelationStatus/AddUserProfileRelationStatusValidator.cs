using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.UserProfiles.AddUserProfileRelationStatus;

public class AddUserProfileRelationStatusValidator : AbstractValidator<AddUserProfileRelationStatusCommand>
{
    public AddUserProfileRelationStatusValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotEmpty();

        RuleFor(x => x.Status)
            .NotEmpty();
    }
}