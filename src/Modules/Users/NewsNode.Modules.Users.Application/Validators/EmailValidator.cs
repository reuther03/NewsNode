using FluentValidation;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Modules.Users.Application.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x).EmailAddress().NotEmpty().Custom((email, context) =>
        {
            if (Email.IsValid(email))
                context.AddFailure("Invalid email");
        });
    }
}