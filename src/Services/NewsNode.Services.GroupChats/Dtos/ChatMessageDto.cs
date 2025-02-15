using NewsNode.Services.GroupChats.GroupChats;

namespace NewsNode.Services.GroupChats.Dtos;

public class ChatMessageDto
{
    public Guid Id { get; init; }
    public string SenderName { get; init; } = null!;
    public string Message { get; init; } = null!;
    public DateTime SentAt { get; init; }

    public static ChatMessageDto AsDto(ChatMessage chatMessage, string senderName)
        => new()
        {
            Id = chatMessage.Id,
            SenderName = senderName,
            Message = chatMessage.Message,
            SentAt = chatMessage.SentAt
        };
}