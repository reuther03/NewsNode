using MediatR;

namespace NewsNode.Shared.Abstractions.Kernel.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;