using NewsNode.Shared.Application.Kernel.Events;

namespace NewsNode.Shared.Application.Kernel.Primitives;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}