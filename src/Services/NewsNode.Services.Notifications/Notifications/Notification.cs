using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Services.Notifications.Notifications;

public sealed class Notification : Entity<Guid>
{
    public Guid ReceiverId { get; private set; }
    public string Title { get; private set; } = null!;
    public string Message { get; private set; } = null!;
    public NotificationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SentAt { get; private set; }

    private Notification()
    {
    }

    public Notification(Guid id, Guid receiverId, string title, string message, DateTime createdAt) : base(id)
    {
        ReceiverId = receiverId;
        Title = title;
        Message = message;
        Status = NotificationStatus.Pending;
        CreatedAt = createdAt;
        SentAt = null;
    }

    public static Notification Create(Guid receiverId, string title, string message)
        => new(Guid.NewGuid(), receiverId, title, message, DateTime.UtcNow);

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = NotificationStatus.Failed;
        SentAt = DateTime.UtcNow;
    }

}