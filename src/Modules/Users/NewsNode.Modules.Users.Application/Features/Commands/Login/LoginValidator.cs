using FluentValidation;
using NewsNode.Modules.Users.Application.Validators;

namespace NewsNode.Modules.Users.Application.Features.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).SetValidator(new EmailValidator());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
    }
}