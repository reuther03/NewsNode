using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Services.Notifications.Notifications;

public sealed class Notification : Entity<Guid>
{
    public Guid ReceiverId { get; private set; }
    public string Title { get; private set; } = null!;
    public NotificationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? ReceivedAt { get; private set; }

    private Notification()
    {
    }

    public Notification(Guid id, Guid receiverId, string title, DateTime createdAt) : base(id)
    {
        ReceiverId = receiverId;
        Title = title;
        Status = NotificationStatus.Pending;
        CreatedAt = createdAt;
        SentAt = null;
        ReceivedAt = null;
    }

    public static Notification Create(Guid receiverId, string title, DateTime createdAt)
        => new(Guid.NewGuid(), receiverId, title, createdAt);
}