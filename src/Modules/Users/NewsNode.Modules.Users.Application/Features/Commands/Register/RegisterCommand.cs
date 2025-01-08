using MediatR;
using NewsNode.Modules.Users.Application.Abstractions;
using NewsNode.Modules.Users.Application.Abstractions.Database;
using NewsNode.Modules.Users.Domain.Users;
using NewsNode.Shared.Abstractions.Events.Integration.Users;
using NewsNode.Shared.Abstractions.Kernel.Primitives.Result;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.QueriesAndCommands.Commands;

namespace NewsNode.Modules.Users.Application.Features.Commands.Register;

public record RegisterCommand : ICommand<Guid>
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Country { get; init; } = null!;
    public string City { get; init; } = null!;

    internal sealed class Handler : ICommandHandler<RegisterCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsWithEmailAsync(request.Email, cancellationToken))
                return Result.BadRequest<Guid>("Email already exists");

            var user = User.Create(
                request.Name,
                request.Email,
                Shared.Abstractions.Kernel.ValueObjects.Password.Create(request.Password),
                new Location(request.Country, request.City)
            );

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            await _publisher.Publish(new UserCreatedEvent(user.Id.Value, user.Email, user.Username, user.Location), cancellationToken);

            return Result.Ok(user.Id.Value);
        }
    }
}