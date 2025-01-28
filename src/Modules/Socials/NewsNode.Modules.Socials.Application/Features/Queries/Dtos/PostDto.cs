using NewsNode.Modules.Socials.Domain.Post;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Modules.Socials.Application.Features.Queries.Dtos;

public class PostDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public DateTime PostedAt { get; init; }
    public Guid CreatedBy { get; init; }
    public int Likes { get; init; }
    public int Bookmarks { get; init; }
    public int Reposts { get; init; }
    public List<string> Hashtags { get; init; } = [];
    public int Comments { get; init; }
    public bool? Seen { get; init; }
    public RecommendationWeight? Weight { get; init; }


    public static PostDto AsDto(Post post, bool? seen, RecommendationWeight? recommendationWeight)
    {
        return new PostDto
        {
            Id = post.Id,
            Content = post.Content,
            PostedAt = post.PostedAt,
            CreatedBy = post.CreatedBy,
            Likes = post.Likes,
            Bookmarks = post.Bookmarks,
            Reposts = post.Reposts,
            Hashtags = post.Hashtags.Select(x => x.Value).ToList(),
            Comments = post.Comments.Count,
            Seen = seen,
            Weight = recommendationWeight
        };
    }

    public static PostDto AsDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Content = post.Content,
            PostedAt = post.PostedAt,
            CreatedBy = post.CreatedBy,
            Likes = post.Likes,
            Bookmarks = post.Bookmarks,
            Reposts = post.Reposts,
            Hashtags = post.Hashtags.Select(x => x.Value).ToList(),
            Comments = post.Comments.Count,
        };
    }
}