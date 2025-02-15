using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.GroupChats.GroupChats;

public class ChatMessage : Entity<Guid>
{
    public string Message { get; private set; }
    public DateTime SentAt { get; private set; }
    public Guid GroupChatId { get; private set; }
    public GroupUser Sender { get; private set; }

    private ChatMessage()
    {
    }


    private ChatMessage(Guid id, string message, DateTime sentAt, Guid groupChatId, GroupUser sender) : base(id)
    {
        Message = message;
        SentAt = sentAt;
        GroupChatId = groupChatId;
        Sender = sender;
    }

    public static ChatMessage Create(string message, Guid groupChatId, GroupUser sender)
        => new(Guid.NewGuid(), message, DateTime.UtcNow, groupChatId, sender);
}