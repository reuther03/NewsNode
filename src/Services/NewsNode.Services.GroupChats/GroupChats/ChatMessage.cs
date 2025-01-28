using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.GroupChats.GroupChats;

public class ChatMessage
{
    public UserId SenderId { get; private set; }
    public string Message { get; private set; }
    public DateTime SentAt { get; private set; }
}