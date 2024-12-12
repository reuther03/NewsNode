using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);
    }
}