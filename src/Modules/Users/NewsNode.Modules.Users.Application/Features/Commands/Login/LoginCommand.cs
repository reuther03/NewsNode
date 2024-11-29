using FluentValidation.Validators;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Auth;
using NewsNode.Shared.Abstractions.Kernel.CommandValidators;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;

namespace NewsNode.Modules.Users.Application.Features.Commands.Login;

public record LoginCommand : ICommand<AccessToken>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;

    public sealed class Handler : ICommandHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public Handler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            NullValidator.ValidateNotNull(user);

            if (!user.Password.Verify(request.Password))
                return Result<AccessToken>.Unauthorized("Invalid email or password");

            var accessToken = AccessToken.Create(
                _jwtProvider.GenerateToken(user.Id.ToString(), user.Email),
                user.Id,
                user.Email
            );

            return Result<AccessToken>.Ok(accessToken);
        }
    }
}