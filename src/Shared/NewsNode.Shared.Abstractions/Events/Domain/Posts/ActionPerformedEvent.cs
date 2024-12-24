using NewsNode.Shared.Abstractions.Kernel.Events;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Events.Domain.Posts;

public record ActionPerformedEvent(PostId PostId, PostActionType ActionType) : IDomainEvent;