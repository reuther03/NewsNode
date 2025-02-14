using NewsNode.Services.GroupChats.GroupChats;

namespace NewsNode.Services.GroupChats.Dtos;

public class ChatMessageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }

    public static ChatMessageDto AsDto(ChatMessage chatMessage, string name)
        => new()
        {
            Id = chatMessage.Id,
            Name = name,
            Message = chatMessage.Message,
            SentAt = chatMessage.SentAt
        };
}