using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Services.GroupChats.Requests;

public record CreateGroupChatRequest
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IList<Hashtag> Hashtags { get; init; } = [];
}