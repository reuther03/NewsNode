using NewsNode.Modules.Socials.Domain.Post;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public class CommentDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public DateTime PostedAt { get; init; }
    public string CreatedBy { get; init; } = null!;
    public int Likes { get; init; }
    public int Reposts { get; init; }
    public int Bookmarks { get; init; }

    public static CommentDto AsDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content ?? string.Empty,
            PostedAt = comment.PostedAt,
            CreatedBy = comment.CreatedBy,
            Likes = comment.Likes,
            Reposts = comment.Reposts,
            Bookmarks = comment.Bookmarks
        };
    }
}