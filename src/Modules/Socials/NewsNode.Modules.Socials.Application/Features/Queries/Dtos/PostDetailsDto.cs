using NewsNode.Modules.Socials.Domain.Post;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public class PostDetailsDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public DateTime PostedAt { get; init; }
    public string CreatedBy { get; init; } = null!;
    public List<Guid> LikeIds { get; init; } = [];
    public List<Guid> RepostIds { get; init; } = [];
    public List<string> Hashtags { get; init; } = [];
    public List<CommentDto> Comments { get; init; } = [];

}