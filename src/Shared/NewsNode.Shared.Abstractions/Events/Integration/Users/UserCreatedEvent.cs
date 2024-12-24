using MediatR;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Events.Integration.Users;

public record UserCreatedEvent(UserId UserId, Email Email, Name UserName) : INotification;