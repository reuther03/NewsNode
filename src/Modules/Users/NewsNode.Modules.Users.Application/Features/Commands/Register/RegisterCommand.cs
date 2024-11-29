using NewsNode.Modules.Users.Application.Abstractions;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Application.Kernel.Primitives.Result;
using NewsNode.Shared.Application.QueriesAndCommands.Commands;

namespace NewsNode.Modules.Users.Application.Features.Commands.Register;

public record RegisterCommand : ICommand<Guid>
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;

    internal sealed class Handler : ICommandHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
                return Result.BadRequest<Guid>("Email already exists");

            var user = User.Create(request.Name, request.Email, Shared.Abstractions.Kernel.ValueObjects.Password.Create(request.Password));

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(user.Id.Value);
        }
    }
}