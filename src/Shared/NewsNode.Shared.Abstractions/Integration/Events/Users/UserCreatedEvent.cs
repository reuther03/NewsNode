using MediatR;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Integration.Events.Users;

public record UserCreatedEvent(UserId UserId, Email Email, Name UserName) : INotification;