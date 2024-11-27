using MediatR;

namespace NewsNode.Shared.Application.Kernel.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;