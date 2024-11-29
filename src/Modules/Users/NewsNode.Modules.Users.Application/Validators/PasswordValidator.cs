using FluentValidation;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Modules.Users.Application.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x).Custom((password, context) =>
        {
            if (!Password.IsValid(password))
                context.AddFailure("Invalid password");
        });
    }
}