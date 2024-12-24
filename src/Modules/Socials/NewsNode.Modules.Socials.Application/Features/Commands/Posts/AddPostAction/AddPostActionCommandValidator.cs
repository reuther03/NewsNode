using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.AddPostAction;

public class AddPostActionCommandValidator : AbstractValidator<AddPostActionCommand>
{
    public AddPostActionCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty();

        RuleFor(x => x.ActionType)
            .IsInEnum();
    }
}