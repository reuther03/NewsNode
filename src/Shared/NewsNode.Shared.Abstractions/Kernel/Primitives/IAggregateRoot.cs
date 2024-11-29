using NewsNode.Shared.Abstractions.Kernel.Events;

namespace NewsNode.Shared.Abstractions.Kernel.Primitives;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}