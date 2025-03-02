using NewsNode.Modules.Socials.Domain.Post;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public class CommentDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public DateTime PostedAt { get; init; }
    public string CreatedBy { get; init; } = null!;
    public Guid PostId { get; init; }
    public int Likes { get; init; }
    public int Reposts { get; init; }
    public int Bookmarks { get; init; }
    public string? ContentImg { get; init; }
    public List<CommentDto> Replies { get; init; } = [];

    public static CommentDto AsDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content ?? string.Empty,
            PostedAt = comment.PostedAt,
            CreatedBy = comment.CreatedBy,
            PostId = comment.PostId,
            Likes = comment.Likes,
            Reposts = comment.Reposts,
            Bookmarks = comment.Bookmarks,
            ContentImg = comment.ContentImg?.FileUrl,
            Replies = comment.Replies.Select(AsDto).ToList()
        };
    }
}