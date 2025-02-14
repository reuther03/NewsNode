using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.GroupChats.GroupChats;

public class ChatMessage : Entity<Guid>
{
    public UserId SenderId { get; private set; }
    public string Message { get; private set; }
    public DateTime SentAt { get; private set; }
    public Guid GroupChatId { get; private set; }

    private ChatMessage()
    {
    }


    private ChatMessage(Guid id, UserId senderId, string message, DateTime sentAt, Guid groupChatId) : base(id)
    {
        SenderId = senderId;
        Message = message;
        SentAt = sentAt;
        GroupChatId = groupChatId;
    }

    public static ChatMessage Create(UserId senderId, string message, Guid groupChatId)
        => new(Guid.NewGuid(), senderId, message, DateTime.UtcNow, groupChatId);
}