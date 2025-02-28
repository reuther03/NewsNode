using FluentValidation;

namespace NewsNode.Modules.Socials.Application.Features.Commands.Posts.AddPostComment;

public class AddPostCommentValidator : AbstractValidator<AddPostCommentCommand>
{
    public AddPostCommentValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
    }
}