using MediatR;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Shared.Abstractions.Integration.Events.Users;

public record UserCreatedEvent(UserId UserId, Email Email) : INotification;